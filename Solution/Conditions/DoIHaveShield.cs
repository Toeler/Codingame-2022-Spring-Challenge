using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class DoIHaveShield : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			return entity.IsShielded;
		}
	}
}
