using System.Collections.Generic;
using Codingame_2022_Spring_Challenge.Commands;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class IsMyTargetCloserToMyBaseThanMe : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			return targetEntity.Position.Distance2To(state.MyBase) <= entity.Position.Distance2To(state.MyBase);
		}
	}
}
