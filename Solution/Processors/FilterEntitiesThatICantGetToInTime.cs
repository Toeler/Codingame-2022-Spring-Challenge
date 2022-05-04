#nullable enable
using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterEntitiesThatICantGetToInTime : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				return false;
			}

			entityCache[CacheKey.TargetEntities] = entities.Where(e => e.GetCollisionTime(state.MyBase, InitialState.BaseRadius) >= entity.GetCollisionTime(e.Position, InitialState.WindSpellRange));
			return true;
		}
	}
}
