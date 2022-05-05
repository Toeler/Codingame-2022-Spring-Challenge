using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterEntitiesThatAreNotMyTarget : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities) &&
			    entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				entityCache[CacheKey.TargetEntities] = entities.Where(e => e != targetEntity);
			}

			return true;
		}
	}
}
