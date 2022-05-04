using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class PushMyTargetTowardsEnemyBase: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			chosenCommands.Add(entity, new UseWindSpellCommand(state.EnemyBase, GetRoleOrDefault(entityCache)));
			return true;
		}
	}
}
