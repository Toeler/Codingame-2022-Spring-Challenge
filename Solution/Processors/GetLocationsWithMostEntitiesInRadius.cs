using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class GetLocationsWithMostEntitiesInRadius: Leaf {
		private int Range { get; }
		private int MinimumEntities { get; }

		public GetLocationsWithMostEntitiesInRadius(int range, int minimumEntities = 1) {
			Range = range;
			MinimumEntities = minimumEntities;
		}

		private static int RoundToNearest(int step, int num) {
			return ((num + (step / 2)) / step) * step;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				IDictionary<Vector, int> points = new Dictionary<Vector, int>();
				Stopwatch sw2 = new Stopwatch();
				sw2.Start();
				foreach (AbstractEntity e in entities) {
					foreach (Vector point in e.Position.GetPointsInRadius(Range, clampToNearest: 50)) {
						if (!points.TryGetValue(point, out int count)) {
							count = 0;
						}
						points[point] = count + 1;
					}
				}
				sw2.Stop();
				Stopwatch sw = new Stopwatch();
				sw.Start();
				IGrouping<int, KeyValuePair<Vector, int>> pointsWithMostEntitiesInRange = points
					.Where(kvp => kvp.Value > MinimumEntities)
					.GroupBy(kvp => kvp.Value)
					.OrderByDescending(g => g.Key)
					.FirstOrDefault();
				sw.Stop();
				IEnumerable<Vector> result = pointsWithMostEntitiesInRange?.Select(kvp => kvp.Key) ?? new List<Vector>();
				entityCache[CacheKey.TargetLocations] = result;
				return result.Any();
			}

			return false;
		}
	}
}
