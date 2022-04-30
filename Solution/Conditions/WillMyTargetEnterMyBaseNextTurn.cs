using System;
using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class WillMyTargetEnterMyBaseNextTurn : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (!cache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			int turnsUntilEntersMyBase = (int)Math.Ceiling(targetEntity.GetCollisionTime(state.MyBase, InitialState.FieldRadius));

			return turnsUntilEntersMyBase == 1;
		}
	}
}
