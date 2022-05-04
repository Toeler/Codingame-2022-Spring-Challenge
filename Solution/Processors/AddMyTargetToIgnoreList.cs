using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class AddMyTargetToIgnoreList : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return true;
			}

			if (!globalCache.TryGetValue(CacheKey.IgnoredEntities, out IDictionary<Hero, IList<AbstractEntity>> ignoredEntitiesByHero)) {
				ignoredEntitiesByHero = new Dictionary<Hero, IList<AbstractEntity>>();
			}

			if (!ignoredEntitiesByHero.TryGetValue(entity, out IList<AbstractEntity> ourIgnoredEntities)) {
				ourIgnoredEntities = new List<AbstractEntity>();
			}

			ourIgnoredEntities.Add(targetEntity);
			ignoredEntitiesByHero[entity] = ourIgnoredEntities;
			globalCache[CacheKey.IgnoredEntities] = ignoredEntitiesByHero;

			return true;
		}
	}
}
