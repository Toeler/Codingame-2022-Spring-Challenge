using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using Lib;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class PushMyTargetTowardsEnemyBase: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity))
			{
				return false;
			}

			Vector pushToLocation = targetEntity.Position + ((state.EnemyBase - targetEntity.Position).Normalize() * InitialState.WindSpellRange) - new Vector(0, targetEntity.Position.Y - entity.Position.Y);

			chosenCommands.Add(entity, new UseWindSpellCommand(pushToLocation.Truncate(), GetRoleOrDefault(entityCache)));
			return true;
		}
	}
}
