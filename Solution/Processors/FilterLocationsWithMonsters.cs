using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterLocationsWithMonsters : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (entityCache.TryGetValue(CacheKey.TargetLocations, out IEnumerable<Vector> locations)) {
				IList<Vector> locationList = locations.ToList();

				entityCache[CacheKey.TargetLocations] = locationList.Where(location => {
					var monstersWithinRange =
						GetMonstersWithRange(state.Monsters, location, InitialState.HeroVisionRange);
					var monstersClosestToLocation = monstersWithinRange.Where(monster =>
						GetClosestPoint(monster.Position, locationList) == location);
					return !monstersClosestToLocation.Any();
				});
			}
			return true;
		}

		private static IEnumerable<Monster> GetMonstersWithRange(IEnumerable<Monster> monsters, Vector point, double range) {
			return monsters.Where(monster => monster.Position.DistanceTo(point) <= range);
		}

		private static Vector GetClosestPoint(Vector point, IEnumerable<Vector> otherPoints) {
			return otherPoints.OrderBy(otherPoint => otherPoint.DistanceTo(point)).FirstOrDefault();
		}
	}
}
