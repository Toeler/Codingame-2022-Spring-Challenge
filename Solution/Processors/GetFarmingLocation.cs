using System.Collections.Generic;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetOffensiveFarmingLocation : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			Vector targetLocation = new Vector(8000, 8500);
			entityCache[CacheKey.TargetLocation] = state.MyBase == Vector.Zero ? targetLocation : state.MyBase - targetLocation;
			return true;
		}
	}

	public class GetMidfieldFarmingLocation : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			Vector targetLocation = new Vector(5000, 8500);
			entityCache[CacheKey.TargetLocation] = state.MyBase == Vector.Zero ? targetLocation : state.MyBase - targetLocation;
			return true;
		}
	}

	public class GetDefensiveFarmingLocation : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			Vector targetLocation = new Vector(7500, 500);
			entityCache[CacheKey.TargetLocation] = state.MyBase == Vector.Zero ? targetLocation : state.MyBase - targetLocation;
			return true;
		}
	}
}
