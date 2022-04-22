using System;

namespace Lib {
	public class MultiMoveSolution<TMove> : ISolution, IComparable<MultiMoveSolution<TMove>> {
		public readonly TMove[] Move;

		public MultiMoveSolution(TMove[] move, double score) {
			Move = move;
			Score = score;
		}

		public double Score { get; }
		public SolutionDebugInfo DebugInfo { get; set; }

		public override string ToString() {
			return $"{Score} {string.Join('|', Move)} {DebugInfo}";
		}

		public int CompareTo(MultiMoveSolution<TMove> other) {
			return Score.CompareTo(other.Score);
		}
	}
}
