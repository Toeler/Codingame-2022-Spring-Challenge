using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class Solver {
		public IEnumerable<Command> GetSolution(State state, Timer timer)
		{
			ISolver<State, MultiMoveSolution<Command>> solver = new MonteCarloSolver().WithLogging(5);

			return solver.GetSolutions(state, timer).Last().Move;
		}
	}
}
