using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class HaveIAlreadyChosenCommand : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			return chosenCommands.ContainsKey(entity);
		}
	}
}
