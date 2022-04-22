using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib {
	public static class SolverLoggingExtension
	{
		public static ISolver<TState, TSolution> WithLogging<TState, TSolution>(this ISolver<TState, TSolution> solver, int bestSolutionsCountToLog) where TSolution : ISolution
		{
			return new LogWrapperSolver<TState, TSolution>(solver, bestSolutionsCountToLog);
		}
	}

	public class LogWrapperSolver<TGameState, TSolution> : ISolver<TGameState, TSolution> where TSolution : ISolution
	{
		private readonly ISolver<TGameState, TSolution> _solver;
		private readonly int _solutionsCountToLog;

		public LogWrapperSolver(ISolver<TGameState, TSolution> solver, int solutionsCountToLog)
		{
			this._solver = solver;
			this._solutionsCountToLog = solutionsCountToLog;
		}

		public string ShortName => _solver.ShortName;

		public override string ToString() => _solver.ToString();

		public IEnumerable<TSolution> GetSolutions(TGameState problem, Timer timer)
		{
			List<TSolution> items = _solver.GetSolutions(problem, timer).ToList();
			List<TSolution> list = items.ToList();
			List<TSolution> bestItems = list.Cast<TSolution>().Reverse().Take(_solutionsCountToLog).ToList();
			Console.Error.WriteLine("## Best found:");
			Console.Error.WriteLine(string.Join(Environment.NewLine, bestItems));
			Console.Error.WriteLine("## Solver debug info:");
			Console.Error.WriteLine(_solver.ToString());
			Console.Error.WriteLine($"Time spent: {timer.TimeElapsed.TotalMilliseconds} ms");
			return list;
		}
	}
}
