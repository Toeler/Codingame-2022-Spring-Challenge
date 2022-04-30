using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetOffensiveFarmingLocation: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			var targetLocation = new Vector(6000, 8000);
			cache[CacheKey.TargetLocation] = state.MyBase == Vector.Zero ? targetLocation : state.MyBase - targetLocation;
			return true;
		}
	}
}
