using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetAttackPosition: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			var targetLocation = new Vector(3800, 600);
			entityCache[CacheKey.TargetLocation] = state.EnemyBase == Vector.Zero ? targetLocation : state.EnemyBase - targetLocation;
			return true;
		}
	}
}
