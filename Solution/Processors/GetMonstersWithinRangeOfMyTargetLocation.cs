using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetMonstersWithinRangeOfMyTargetLocation: Leaf {
		private int Range { get; }

		public GetMonstersWithinRangeOfMyTargetLocation(int range) {
			Range = range;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetLocation, out Vector targetLocation)) {
				return false;
			}
			entityCache[CacheKey.TargetEntities] = state.Monsters.Where(monster => monster.Position.DistanceTo(targetLocation) <= Range);
			return true;
		}
	}
}
