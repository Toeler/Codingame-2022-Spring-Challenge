using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using Lib;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public abstract class AmIWithinRangeOfBase: Leaf {
		private double Range { get; }
		protected abstract Vector GetBase(State state);

		public AmIWithinRangeOfBase(double range) {
			Range = range;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			return entity.Position.DistanceTo(GetBase(state)) <= Range;
		}
	}

	public class AmIWithinRangeOfMyBase : AmIWithinRangeOfBase {
		public AmIWithinRangeOfMyBase(double range) : base(range) {
		}

		protected override Vector GetBase(State state) {
			return state.MyBase;
		}
	}

	public class AmIWithinRangeOfEnemyBase : AmIWithinRangeOfBase {
		public AmIWithinRangeOfEnemyBase(double range) : base(range) {
		}

		protected override Vector GetBase(State state) {
			return state.EnemyBase;
		}
	}
}
