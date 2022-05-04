using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterEntitiesWithShield: Leaf {
		public int MinimumShield { get; }

		public FilterEntitiesWithShield(int minimumShield = 1) {
			MinimumShield = minimumShield;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				entityCache[CacheKey.TargetEntities] = entities.Where(e => e.ShieldLife < MinimumShield);
			}
			return true;
		}
	}
}
