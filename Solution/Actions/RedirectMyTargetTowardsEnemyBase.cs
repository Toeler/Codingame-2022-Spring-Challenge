using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class RedirectMyTargetTowardsEnemyBase : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			if (!(targetEntity is Monster target)) {
				return false;
			}

			chosenCommands.Add(entity, new UseControlSpellCommand(target, state.EnemyBase, GetRoleOrDefault(entityCache)));
			return true;
		}
	}
}
