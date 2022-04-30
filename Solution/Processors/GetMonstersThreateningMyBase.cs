using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetMonstersThreateningMyBase: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			cache[CacheKey.TargetEntities] = state.MonstersThreateningMyBase;
			return true;
		}
	}
}
