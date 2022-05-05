using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetMonstersWithinRangeOfMyBase : Leaf {
		public GetMonstersWithinRangeOfMyBase(int range) {
			Range = range;
		}

		public int Range { get; }

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			entityCache[CacheKey.TargetEntities] = state.Monsters.Where(monster => monster.Position.DistanceTo(state.MyBase) <= Range);
			return true;
		}
	}
}
