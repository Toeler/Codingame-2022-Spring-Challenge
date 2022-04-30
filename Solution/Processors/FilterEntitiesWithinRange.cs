#nullable enable
using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public abstract class FilterEntitiesWithinRange : Leaf {
		public int Range { get; }

		protected FilterEntitiesWithinRange(int range) {
			Range = range;
		}

		protected abstract Vector? GetLocation(Hero entity, State state, BehaviourCache cache);


		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			var targetLocation = GetLocation(entity, state, cache);
			if (targetLocation == null || !cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				return true;
			}

			cache[CacheKey.TargetEntities] = entities.Where(e => e.Position.DistanceTo(targetLocation) <= Range);
			return true;
		}
	}

	public class FilterEntitiesWithinRangeOfMyTargetLocation : FilterEntitiesWithinRange {
		public FilterEntitiesWithinRangeOfMyTargetLocation(int range): base(range) {
		}

		protected override Vector? GetLocation(Hero entity, State state, BehaviourCache cache) {
			cache.TryGetValue(CacheKey.TargetLocation, out Vector targetLocation);
			return targetLocation;
		}
	}

	public class FilterEntitiesWithinRangeOfMe: FilterEntitiesWithinRange {
		public FilterEntitiesWithinRangeOfMe(int range) : base(range) {
		}

		protected override Vector? GetLocation(Hero entity, State state, BehaviourCache cache) {
			return entity.Position;
		}
	}

	public class FilterEntitiesWithinRangeOfMyBase : FilterEntitiesWithinRange {
		public FilterEntitiesWithinRangeOfMyBase(int range) : base(range) {
		}

		protected override Vector? GetLocation(Hero entity, State state, BehaviourCache cache) {
			return state.MyBase;
		}
	}
}
