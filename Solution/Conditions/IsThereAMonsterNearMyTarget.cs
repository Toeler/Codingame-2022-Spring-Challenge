using System;
using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class IsThereAMonsterNearMyTarget : Leaf {
		public IsThereAMonsterNearMyTarget(int range) {
			Range = range;
		}

		private int Range { get; }

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			return state.Monsters.Where(monster => monster != targetEntity).Select(monster =>
				targetEntity.Position.Distance2To(monster.Position) <= Math.Pow(Range, 2)
			).Any(isInRange => isInRange);
		}
	}
}
