using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge.Processors {
	public abstract class GetMonstersThreateningBase: Leaf {
		private Target Target { get; }

		protected GetMonstersThreateningBase(Target target) {
			Target = target;
		}

		protected abstract Vector GetBasePos(State state);

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			entityCache[CacheKey.TargetEntities] = state.Monsters.Where(m => m.Target == Target)
				.OrderBy(m => m.GetCollisionTimeToBase(GetBasePos(state), InitialState.FieldRadius, InitialState.BaseRadius))
				.ToList();
			return true;
		}
	}

	public class GetMonstersThreateningMyBase: GetMonstersThreateningBase {
		public GetMonstersThreateningMyBase() : base(Target.MyBase) { }
		protected override Vector GetBasePos(State state) {
			return state.MyBase;
		}
	}

	public class GetMonstersThreateningEnemyBase: GetMonstersThreateningBase {
		public GetMonstersThreateningEnemyBase() : base(Target.EnemyBase) { }
		protected override Vector GetBasePos(State state) {
			return state.EnemyBase;
		}
	}
}
