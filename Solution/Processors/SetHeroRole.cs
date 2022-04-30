using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class SetHeroRole: Leaf {
		public HeroRole Role { get; }

		public SetHeroRole(HeroRole role) {
			Role = role;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			cache[CacheKey.Role] = Role;
			return true;
		}
	}
}
