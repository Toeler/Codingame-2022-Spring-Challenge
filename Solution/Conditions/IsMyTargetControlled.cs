using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class IsMyTargetControlled: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (!cache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			return targetEntity.IsControlled;
		}
	}
}
