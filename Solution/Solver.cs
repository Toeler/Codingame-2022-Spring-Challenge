using System;
using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class Solver {
		public IEnumerable<Command> GetSolution(State state, Timer timer)
		{
			ISolver<State, SingleMoveSolution<IDictionary<Hero, AbstractCommand>>> solver = new BehaviourTreeSolver().WithLogging(5, false);

			IDictionary<Hero, AbstractCommand> solution = solver.GetSolutions(state, timer).First().Move;

			return state.MyHeroes.Select(hero => {
				if (!solution!.TryGetValue(hero, out var command)) {
					throw new Exception($"Failed to find command for hero {hero.Id}");
				}
				return command;
			});
		}
	}
}
