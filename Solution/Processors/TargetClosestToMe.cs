using System.Collections;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public abstract class TargetClosestToMe<T> : Leaf {
		public CacheKey ListKey { get; }
		public CacheKey TargetKey { get; }

		protected TargetClosestToMe(CacheKey listKey, CacheKey targetKey) {
			ListKey = listKey;
			TargetKey = targetKey;
		}

		protected abstract Vector GetTargetLocation(T target);

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (!cache.TryGetValue(ListKey, out IEnumerable<T> targets)) {
				return false;
			}

			cache[TargetKey] = targets.OrderBy(target => GetTargetLocation(target).DistanceTo(entity.Position)).FirstOrDefault();
			return cache[TargetKey] != null;
		}
	}

	public class TargetLocationClosestToMe : TargetClosestToMe<Vector> {
		public TargetLocationClosestToMe() : base(CacheKey.TargetLocations, CacheKey.TargetLocation) {
		}

		protected override Vector GetTargetLocation(Vector target) {
			return target;
		}
	}

	public class TargetEntityClosestToMe : TargetClosestToMe<AbstractEntity> {
		public TargetEntityClosestToMe() : base(CacheKey.TargetEntities, CacheKey.TargetEntity) {
		}
		
		protected override Vector GetTargetLocation(AbstractEntity target) {
			return target.Position;
		}
	}
}
