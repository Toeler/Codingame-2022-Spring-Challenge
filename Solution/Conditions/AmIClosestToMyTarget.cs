using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public abstract class AmIClosestToMyTarget : Leaf {
		protected CacheKey CacheKey { get; }

		protected AmIClosestToMyTarget(CacheKey cacheKey) {
			CacheKey = cacheKey;
		}

		protected abstract bool TryGetTargetLocation(BehaviourCache cache, out Vector targetLocation);
		protected virtual bool ShouldConsiderHero(Hero hero, BehaviourCache globalCache, BehaviourCache entityCache) => true;

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!TryGetTargetLocation(entityCache, out Vector targetLocation)) {
				return false;
			}
			if (!globalCache.TryGetValue(CacheKey.WaitingHeroes, out IList<Hero> waitingHeroes)) {
				waitingHeroes = new List<Hero>();
			}

			var myOtherHeroesWithoutCommands = state.MyHeroes.Where(otherHero => otherHero != entity && !chosenCommands.ContainsKey(otherHero) && !waitingHeroes.Contains(otherHero) && ShouldConsiderHero(otherHero, globalCache, entityCache));
			double myDistanceToTarget = entity.Position.DistanceTo(targetLocation);

			return !myOtherHeroesWithoutCommands.Any(otherHero =>
				otherHero.Position.DistanceTo(targetLocation) < myDistanceToTarget);
		}
	}

	public class AmIClosestToMyTargetLocation : AmIClosestToMyTarget {
		public AmIClosestToMyTargetLocation() : base(CacheKey.TargetLocation) {
		}

		protected override bool TryGetTargetLocation(BehaviourCache cache, out Vector targetLocation) {
			return cache.TryGetValue(CacheKey, out targetLocation);
		}
	}

	public class AmIClosestToMyTargetEntity: AmIClosestToMyTarget {
		public AmIClosestToMyTargetEntity() : base(CacheKey.TargetEntity) {
		}

		protected override bool TryGetTargetLocation(BehaviourCache cache, out Vector targetLocation) {
			if (cache.TryGetValue(CacheKey, out AbstractEntity entity)) {
				targetLocation = entity.Position;
				return true;
			}

			targetLocation = null;
			return false;
		}

		protected override bool ShouldConsiderHero(Hero hero, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey, out AbstractEntity entity)) {
				return true;
			}
			if (!globalCache.TryGetValue(CacheKey.IgnoredEntities, out IDictionary<Hero, IList<AbstractEntity>> ignoredEntitiesByHero)) {
				return true;
			}
			if (!ignoredEntitiesByHero.TryGetValue(hero, out IList<AbstractEntity> ourIgnoredEntities)) {
				return true;
			}

			return !ourIgnoredEntities.Contains(entity);
		}
	}
}
