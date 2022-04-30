using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class WaitAction : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			chosenCommands.Add(entity, new WaitCommand(GetRoleOrDefault(cache)));
			return true;
		}
	}
}
