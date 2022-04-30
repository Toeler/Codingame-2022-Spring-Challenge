using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using Lib;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class MoveToMyTargetLocation : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (!cache.TryGetValue(CacheKey.Role, out HeroRole role)) {
				role = HeroRole.None;
			}
			if (!cache.TryGetValue(CacheKey.TargetLocation, out Vector targetLocation)) {
				return false;
			}
			chosenCommands.Add(entity, new MoveCommand(targetLocation, role));
			return true;
		}
	}
}
