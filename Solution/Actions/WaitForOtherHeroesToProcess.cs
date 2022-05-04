using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class WaitForOtherHeroesToProcess : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!globalCache.TryGetValue(CacheKey.WaitingHeroes, out IList<Hero> waitingHeroes)) {
				waitingHeroes = new List<Hero>();
			}
			waitingHeroes.Add(entity);
			globalCache[CacheKey.WaitingHeroes] = waitingHeroes;
			return true;
		}
	}
}
