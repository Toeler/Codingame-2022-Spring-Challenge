using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterMonstersWithThreatFor: Leaf {
		public Target Target { get; }

		public FilterMonstersWithThreatFor(Target target) {
			Target = target;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				cache[CacheKey.TargetEntities] = entities.OfType<Monster>().Where(monster => monster.Target != Target);
			}
			return true;
		}
	}
}
