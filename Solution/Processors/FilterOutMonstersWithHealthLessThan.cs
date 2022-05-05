using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterOutMonstersWithHealthLessThan : Leaf {
		public FilterOutMonstersWithHealthLessThan(int health) {
			Health = health;
		}

		public int Health { get; }

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				entityCache[CacheKey.TargetEntities] = entities.OfType<Monster>().Where(monster => monster.Health >= Health);
			}

			return true;
		}
	}
}
