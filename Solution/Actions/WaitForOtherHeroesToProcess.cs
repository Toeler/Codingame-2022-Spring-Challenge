using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class WaitForOtherHeroesToProcess : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			return true;
		}
	}
}
