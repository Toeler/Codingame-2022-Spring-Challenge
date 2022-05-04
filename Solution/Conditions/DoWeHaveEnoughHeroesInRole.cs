using Codingame_2022_Spring_Challenge.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class DoWeHaveEnoughHeroesInRoles : Leaf {
		private IReadOnlyCollection<HeroRole> Roles { get; }
		private int MaximumOfRole { get; }

		public DoWeHaveEnoughHeroesInRoles(IReadOnlyCollection<HeroRole> roles, int maximumOfRole) {
			Roles = roles;
			MaximumOfRole = maximumOfRole;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			int numberOfHeroesInRole = chosenCommands.Count(kvp => Roles.Contains(kvp.Value.Role));
			return numberOfHeroesInRole >= MaximumOfRole;
		}
	}

	public class DoWeHaveEnoughHeroesInRole : DoWeHaveEnoughHeroesInRoles {
		public DoWeHaveEnoughHeroesInRole(HeroRole role, int maximumOfRole): base(new List<HeroRole> { role }, maximumOfRole) {
		}
	}
}
