using System;
using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetMonstersThatWillReachEnemyBaseInTurns : Leaf {
		private int Turns { get; }

		public GetMonstersThatWillReachEnemyBaseInTurns(int turns) {
			Turns = turns;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			entityCache[CacheKey.TargetEntities] = state.Monsters.Where(monster => Math.Ceiling(monster.GetCollisionTime(state.EnemyBase, InitialState.BaseRadius)) <= Turns);
			return true;
		}
	}
}
