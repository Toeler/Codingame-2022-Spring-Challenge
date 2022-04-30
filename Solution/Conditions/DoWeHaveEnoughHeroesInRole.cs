using Codingame_2022_Spring_Challenge.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class DoWeHaveEnoughHeroesInRole : Leaf {
		public HeroRole Role { get; }
		public int MaximumOfRole { get; }

		public DoWeHaveEnoughHeroesInRole(HeroRole role, int maximumOfRole) {
			Role = role;
			MaximumOfRole = maximumOfRole;
		}

		public bool ExecuteOld(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			int numberOfEntitiesToDealWith = 0;
			if (cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entitiesToTarget)) {
				numberOfEntitiesToDealWith = entitiesToTarget.Count();
			}

			int myAvailableHeroes = state.MyHeroes.Count(h => !h.IsControlled);
			int numberOfHeroesInRole = chosenCommands.Count(kvp => kvp.Key != entity && kvp.Value.Role == Role);

			int numberOfHeroesRequired = Math.Min(myAvailableHeroes, numberOfEntitiesToDealWith);
			return numberOfHeroesInRole >= numberOfHeroesRequired && numberOfHeroesInRole < MaximumOfRole;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			int numberOfHeroesInRole = chosenCommands.Count(kvp => kvp.Value.Role == Role);
			return numberOfHeroesInRole >= MaximumOfRole;
		}
	}
}
