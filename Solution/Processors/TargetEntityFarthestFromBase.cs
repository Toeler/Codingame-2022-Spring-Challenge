using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public abstract class TargetEntityFarthestFromBase : Leaf {
		protected abstract Vector GetBase(State state);

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (!cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				return false;
			}

			cache[CacheKey.TargetEntity] = entities.OrderByDescending(e => e.Position.DistanceTo(GetBase(state))).FirstOrDefault();
			return cache[CacheKey.TargetEntity] != null;
		}
	}

	public class TargetEntityFarthestFromMyBase : TargetEntityFarthestFromBase {
		protected override Vector GetBase(State state) => state.MyBase;
	}

	public class TargetEntityFarthestFromEnemyBase : TargetEntityFarthestFromBase {
		protected override Vector GetBase(State state) => state.EnemyBase;
	}
}