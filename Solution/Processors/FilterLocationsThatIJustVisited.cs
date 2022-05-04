using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterLocationsThatIJustVisited: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (entityCache.TryGetValue(CacheKey.TargetLocations, out IEnumerable<Vector> locations)) {
				Vector closestLocation = locations.OrderBy(location => location.DistanceTo(entity.Position)).First();

				if (closestLocation == null) {
					return false;
				}

				bool isHeroFacingLocation = entity.Vector.DotMultiply(closestLocation - entity.Position) >= 0;

				if (!isHeroFacingLocation) {
					entityCache[CacheKey.TargetLocations] = locations.Where(location => location != closestLocation);
				}
			}
			return true;
		}
	}
}
