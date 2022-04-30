using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterShieldedMonstersThatWillReachMyBase : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				cache[CacheKey.TargetEntities] = entities.Where(monster =>
					monster.GetCollisionTimeToBase(state.MyBase, InitialState.FieldRadius, InitialState.BaseRadius) >= monster.ShieldLife);
			}
			return true;
		}
	}
}
