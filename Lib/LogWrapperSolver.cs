using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib {
	public static class SolverLoggingExtension
	{
		public static ISolver<TState, TSolution> WithLogging<TState, TSolution>(this ISolver<TState, TSolution> solver, int bestSolutionsCountToLog, bool shouldReverse = true) where TSolution : ISolution
		{
			return new LogWrapperSolver<TState, TSolution>(solver, bestSolutionsCountToLog, shouldReverse);
		}
	}

	public class LogWrapperSolver<TGameState, TSolution> : ISolver<TGameState, TSolution> where TSolution : ISolution
	{
		private readonly ISolver<TGameState, TSolution> _solver;
		private readonly int _solutionsCountToLog;
		private readonly bool _shouldReverse;

		public LogWrapperSolver(ISolver<TGameState, TSolution> solver, int solutionsCountToLog, bool shouldReverse = true)
		{
			_solver = solver;
			_solutionsCountToLog = solutionsCountToLog;
			_shouldReverse = shouldReverse;
		}

		public string ShortName => _solver.ShortName;

		public override string ToString() => _solver.ToString();

		public IEnumerable<TSolution> GetSolutions(TGameState problem, Timer timer)
		{
			List<TSolution> items = _solver.GetSolutions(problem, timer).ToList();
			List<TSolution> list = items.ToList();
			IEnumerable<TSolution> bestItems = list.Cast<TSolution>();
			if (_shouldReverse) {
				bestItems = bestItems.Reverse();
			}
			bestItems = bestItems.Take(_solutionsCountToLog).ToList();
			Console.Error.WriteLine("## Best found:");
			Console.Error.WriteLine(string.Join(Environment.NewLine, bestItems));
			Console.Error.WriteLine("## Solver debug info:");
			Console.Error.WriteLine(_solver.ToString());
			Console.Error.WriteLine($"Time spent: {timer.TimeElapsed.TotalMilliseconds} ms");
			return list;
		}
	}
}
