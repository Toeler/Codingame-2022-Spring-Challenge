using System;
using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class CanIGetInRangeOfMyTargetBeforeItReachesMyBase : Leaf {
		private int Range { get; }

		public CanIGetInRangeOfMyTargetBeforeItReachesMyBase(int range) {
			Range = range;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			if (!(targetEntity is Monster monster)) {
				return false;
			}

			double turnsUntilEntersMyBase = Math.Ceiling(monster.GetCollisionTime(state.MyBase, InitialState.BaseRadius));
			double turnsUntilIReachMyTarget = Math.Ceiling(entity.GetCollisionTime(monster.Position, Range));

			return turnsUntilEntersMyBase > turnsUntilIReachMyTarget;
		}
	}
}
