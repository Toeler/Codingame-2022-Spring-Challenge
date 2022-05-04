using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class IsItAtLeastTurnNumber : Leaf {
		private int Number { get; }

		public IsItAtLeastTurnNumber(int number) {
			Number = number;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			return state.TurnNumber >= Number;
		}
	}
}
