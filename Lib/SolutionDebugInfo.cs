using System;
using System.Text;

namespace Lib {
	public class SolutionDebugInfo {
		public TimeSpan Time { get; }
		public int Index { get; }
		public int ImprovementIndex { get; }
		public string SolverName { get; }

		public SolutionDebugInfo(Timer timer, int index, int improvementIndex, string solverName)
			: this(timer.TimeElapsed, index, improvementIndex, solverName) { }

		public SolutionDebugInfo(TimeSpan time, int index, int improvementIndex, string solverName) {
			Time = time;
			Index = index;
			ImprovementIndex = improvementIndex;
			SolverName = solverName;
		}

		public override string ToString() {
			StringBuilder result = new StringBuilder(Time.ToString());

			if (ImprovementIndex > 0 || Index > 0) {
				result.Append($" improvement {ImprovementIndex} of {Index}");
			}

			if (!string.IsNullOrEmpty(SolverName)) {
				result.Append($" by {SolverName}");
			}

			return result.ToString();
		}
	}
}
