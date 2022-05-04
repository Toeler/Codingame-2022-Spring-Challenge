using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using Lib;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public abstract class AmIWithinRangeOfMyTarget<T> : Leaf {
		private CacheKey TargetKey { get; }
		private double Range { get; }

		protected AmIWithinRangeOfMyTarget(CacheKey targetKey, double range) {
			TargetKey = targetKey;
			Range = range;
		}

		protected abstract Vector GetTargetLocation(T target);

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(TargetKey, out T target)) {
				return false;
			}

			return GetTargetLocation(target).DistanceTo(entity.Position) <= Range;
		}
	}

	public class AmIWithinRangeOfMyTargetEntity : AmIWithinRangeOfMyTarget<AbstractEntity> {

		public AmIWithinRangeOfMyTargetEntity(double range) : base(CacheKey.TargetEntity, range) {
		}

		protected override Vector GetTargetLocation(AbstractEntity target) {
			return target.Position;
		}
	}

	public class AmIWithinRangeOfMyTargetLocation : AmIWithinRangeOfMyTarget<Vector> {

		public AmIWithinRangeOfMyTargetLocation(double range) : base(CacheKey.TargetLocation, range) {
		}

		protected override Vector GetTargetLocation(Vector target) {
			return target;
		}
	}
}
