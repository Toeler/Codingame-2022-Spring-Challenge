using System.Collections;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public abstract class TargetClosest<T> : Leaf {
		public CacheKey ListKey { get; }
		public CacheKey TargetKey { get; }

		protected TargetClosest(CacheKey listKey, CacheKey targetKey) {
			ListKey = listKey;
			TargetKey = targetKey;
		}

		protected abstract Vector GetTargetLocation(T target);
		protected abstract bool TryGetOriginLocation(Hero entity, BehaviourCache cache, out Vector origin);

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(ListKey, out IEnumerable<T> targets) || !TryGetOriginLocation(entity, entityCache, out Vector origin)) {
				return false;
			}

			entityCache[TargetKey] = targets.OrderBy(target => GetTargetLocation(target).DistanceTo(origin)).FirstOrDefault();
			return entityCache[TargetKey] != null;
		}
	}

	public class TargetLocationClosestToMe : TargetClosest<Vector> {
		public TargetLocationClosestToMe() : base(CacheKey.TargetLocations, CacheKey.TargetLocation) {
		}

		protected override Vector GetTargetLocation(Vector target) {
			return target;
		}

		protected override bool TryGetOriginLocation(Hero entity, BehaviourCache cache, out Vector origin) {
			origin = entity.Position;
			return true;
		}
	}

	public class TargetEntityClosestToMe : TargetClosest<AbstractEntity> {
		public TargetEntityClosestToMe() : base(CacheKey.TargetEntities, CacheKey.TargetEntity) {
		}

		protected override Vector GetTargetLocation(AbstractEntity target) {
			return target.Position;
		}

		protected override bool TryGetOriginLocation(Hero entity, BehaviourCache cache, out Vector origin) {
			origin = entity.Position;
			return true;
		}
	}

	public class TargetLocationClosestToMyTarget : TargetClosest<Vector> {
		public TargetLocationClosestToMyTarget() : base(CacheKey.TargetLocations, CacheKey.TargetLocation) {
		}

		protected override Vector GetTargetLocation(Vector target) {
			return target;
		}

		protected override bool TryGetOriginLocation(Hero entity, BehaviourCache cache, out Vector origin) {
			return cache.TryGetValue(CacheKey.TargetLocation, out origin);
		}
	}
}
