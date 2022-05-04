using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetMonstersWithinRangeOfEnemyBase: Leaf {
		public int Range { get; }

		public GetMonstersWithinRangeOfEnemyBase(int range) {
			Range = range;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			entityCache[CacheKey.TargetEntities] = state.Monsters.Where(monster => monster.Position.DistanceTo(state.EnemyBase) <= Range);
			return true;
		}
	}
}
