using System;
using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class CanIKillMyTargetBeforeItReachesMyBase : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			if (!(targetEntity is Monster monster)) {
				return false;
			}

			double turnsUntilEntersMyBase = Math.Ceiling(monster.GetCollisionTime(state.MyBase, InitialState.BaseRadius));
			double turnsUntilIReachMyTarget = Math.Ceiling(entity.GetCollisionTime(monster.Position, InitialState.HeroAttackRadius));
			double turnsToKillMyTarget = Math.Ceiling((double)monster.Health / InitialState.HeroDamagePerTurn);

			return turnsUntilEntersMyBase >= turnsUntilIReachMyTarget + turnsToKillMyTarget;
		}
	}
}
