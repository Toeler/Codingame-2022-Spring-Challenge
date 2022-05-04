using Codingame_2022_Spring_Challenge.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class IsThereEnemyHeroNearMyTarget : Leaf {
		private int Range { get; }

		public IsThereEnemyHeroNearMyTarget(int range) {
			Range = range;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			return state.EnemyHeroes.Select(enemyHero =>
				targetEntity.Position.Distance2To(enemyHero.Position) <= Math.Pow(Range, 2)
			).Any(isInRange => isInRange);
		}
	}
}
