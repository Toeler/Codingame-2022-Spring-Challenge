using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetEnemyHeroesNearMyBase: Leaf {
		public int Range { get; }

		public GetEnemyHeroesNearMyBase(int range) {
			Range = range;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			entityCache[CacheKey.TargetEntities] = state.EnemyHeroes.Where(enemy => enemy.Position.DistanceTo(state.MyBase) <= Range);
			return true;
		}
	}
}
