using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class TargetMonsterAboutToLeaveTheMap : Leaf {
		private int Turns { get; }

		public TargetMonsterAboutToLeaveTheMap(int turns) {
			Turns = turns;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (!cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				return false;
			}

			cache[CacheKey.TargetEntity] = entities
				.Where(e => e.Position.GetExitTime(e.Vector, InitialState.TopLeft, InitialState.BottomRight) <= Turns)
				.OrderBy(e => e.Position.GetExitTime(e.Vector, InitialState.TopLeft, InitialState.BottomRight))
				.FirstOrDefault();
			return cache[CacheKey.TargetEntity] != null;
		}
	}
}
