using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class IsMyTargetMonsterHealthLessThan : Leaf {
		private int MaxHealth { get; }

		public IsMyTargetMonsterHealthLessThan(int maxHealth) {
			MaxHealth = maxHealth;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity) || !(targetEntity is Monster monster)) {
				return false;
			}

			return monster.Health <= MaxHealth;
		}
	}
}
