using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public abstract class TargetNextEntityClosestToBase : Leaf {
		protected abstract Vector GetBase(State state);

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				return false;
			}
			if (!entityCache.TryGetValue(CacheKey.TargetEntityNumber, out int entityNumber)) {
				entityNumber = 0;
			}

			entityCache[CacheKey.TargetEntity] = entities.OrderBy(e => e.Position.DistanceTo(GetBase(state))).ElementAtOrDefault(entityNumber);
			entityCache[CacheKey.TargetEntityNumber] = entityNumber + 1;
			return entityCache[CacheKey.TargetEntity] != null;
		}
	}

	public class TargetNextEntityClosestToMyBase : TargetNextEntityClosestToBase {
		protected override Vector GetBase(State state) => state.MyBase;
	}

	public class TargetFirstEntityClosestToMyBase : TargetNextEntityClosestToBase {
		protected override Vector GetBase(State state) => state.MyBase;

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			entityCache[CacheKey.TargetEntityNumber] = 0;
			return base.Execute(entity, state, chosenCommands, globalCache, entityCache);
		}
	}

	public class TargetNextEntityClosestToEnemyBase : TargetNextEntityClosestToBase {
		protected override Vector GetBase(State state) => state.EnemyBase;
	}

	public class TargetFirstEntityClosestToEnemyBase : TargetNextEntityClosestToBase {
		protected override Vector GetBase(State state) => state.EnemyBase;

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			entityCache[CacheKey.TargetEntityNumber] = 0;
			return base.Execute(entity, state, chosenCommands, globalCache, entityCache);
		}
	}
}
