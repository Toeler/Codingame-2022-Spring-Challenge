using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterOutEntitiesThatArentTargetingMyBase : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				return false;
			}

			entityCache[CacheKey.TargetEntities] = entities.OfType<Monster>().Where(monster => monster.Target == Target.MyBase);
			return true;
		}
	}
}
