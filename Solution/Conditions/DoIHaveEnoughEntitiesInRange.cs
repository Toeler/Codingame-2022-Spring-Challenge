using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class DoIHaveEnoughEntitiesInRange : Leaf {
		public int MinimumCount { get; }

		public DoIHaveEnoughEntitiesInRange(int minimumCount) {
			MinimumCount = minimumCount;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> targets)) {
				return targets.Count() >= MinimumCount;
			}

			return false;
		}
	}
}
