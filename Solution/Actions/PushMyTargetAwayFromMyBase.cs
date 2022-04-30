using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class PushMyTargetAwayFromMyBase : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			Vector pushToLocation = entity.Position + ((entity.Position - state.MyBase).Normalize() * InitialState.WindSpellRange);
			
			chosenCommands.Add(entity, new UseWindSpellCommand(pushToLocation.Truncate(), GetRoleOrDefault(cache)));
			return true;
		}
	}
}
