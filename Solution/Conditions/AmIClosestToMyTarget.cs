using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public abstract class AmIClosestToMyTarget : Leaf {
		public CacheKey CacheKey { get; }

		protected AmIClosestToMyTarget(CacheKey cacheKey) {
			CacheKey = cacheKey;
		}

		protected abstract bool TryGetTargetLocation(BehaviourCache cache, out Vector targetLocation);

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (!TryGetTargetLocation(cache, out Vector targetLocation)) {
				return false;
			}

			var myOtherHeroesWithoutCommands = state.MyHeroes.Where(otherHero => otherHero != entity && !chosenCommands.ContainsKey(otherHero));
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
	}
}
