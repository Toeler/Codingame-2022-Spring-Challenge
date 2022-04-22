using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib {
	public abstract class AbstractMonteCarloSolver<TProblem, TSolution> : ISolver<TProblem, TSolution> where TSolution : ISolution {
		private readonly StatValue _improvementsCount = StatValue.CreateEmpty("Improvements");
		private readonly StatValue _simulationsCount = StatValue.CreateEmpty("Simulations");
		private readonly StatValue _timeToFindBestMs = StatValue.CreateEmpty("TimeOfBestMs");

		public override string ToString() {
			return string.Join(Environment.NewLine, _simulationsCount, _improvementsCount, _timeToFindBestMs);
		}

		public string ShortName => "MC";

		protected abstract TSolution GenerateRandomSolution(TProblem problem);

		public IEnumerable<TSolution> GetSolutions(TProblem problem, Timer timer) {
			int simCount = 0;
			int improvementsCount = 0;
			double bestScore = double.NegativeInfinity;
			List<TSolution> steps = new List<TSolution>();

			while (!timer.IsFinished()) {
				TSolution solution = GenerateRandomSolution(problem);
				simCount++;
				if (solution.Score > bestScore) {
					improvementsCount++;
					bestScore = solution.Score;
					solution.DebugInfo = new SolutionDebugInfo(timer, simCount, improvementsCount, ShortName);
					steps.Add(solution);
				}
			}

			_simulationsCount.Add(simCount);
			_improvementsCount.Add(improvementsCount);
			if (steps.Count > 0) {
				_timeToFindBestMs.Add(steps.Last().DebugInfo.Time.TotalMilliseconds);
			}

			return steps;
		}
	}
}
