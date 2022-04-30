using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetPatrolLocations: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			cache[CacheKey.TargetLocations] = state.PatrolLocations;
			return true;
		}
	}
}
