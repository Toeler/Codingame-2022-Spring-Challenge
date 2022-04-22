using System;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class MonteCarloSolver: AbstractMonteCarloSolver<State, MultiMoveSolution<Command>> {
		private readonly Random _rand = new Random();

		protected override MultiMoveSolution<Command> GenerateRandomSolution(State problem) {
			IList<IEnumerable<Command>> actions = problem.GetPossibleCommands();
			IEnumerable<Command> selectedAction = actions[_rand.Next(actions.Count)];

			// Simulate Command
			double score = problem.ScoreCommand(selectedAction);

			return new MultiMoveSolution<Command>(selectedAction.ToArray(), score);
		}
	}
}
