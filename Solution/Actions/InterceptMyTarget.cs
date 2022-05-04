using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class InterceptMyTarget : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.Role, out HeroRole role)) {
				role = HeroRole.None;
			}
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}
			chosenCommands.Add(entity, new InterceptCommand(entity, targetEntity, role));
			return true;
		}
	}
}
