using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class IsMyTargetWithinRangeOfMyBase : Leaf {
		private double Range { get; }

		public IsMyTargetWithinRangeOfMyBase(double range) {
			Range = range;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			return targetEntity.Position.DistanceTo(state.MyBase) <= Range;
		}
	}
}
