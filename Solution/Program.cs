using System;
using System.Collections.Generic;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	internal static class Program {
		private static readonly TimeSpan FirstTurnTime = TimeSpan.FromMilliseconds(950);
		private static readonly TimeSpan TurnTime = TimeSpan.FromMilliseconds(40);

		private static void Main(string[] args) {
			ConsoleReader consoleReader = new ConsoleReader();
			StateReader stateReader = new StateReader(consoleReader);
			Solver solver = new Solver();

			InitialState initialState = stateReader.ReadInitialState();
			consoleReader.Flush();

			int turn = 0;
			State state = null;
			while (true) {
				turn++;
				state = stateReader.ReadTurn(turn, initialState, state);

				Timer timer = new Timer(turn == 1 ? FirstTurnTime : TurnTime); //TODO start this after the first input read, not the last
				consoleReader.Flush();

				IEnumerable<Command> command = solver.GetSolution(state, timer);
				Console.WriteLine(string.Join(Environment.NewLine, command));
				Console.Error.WriteLine(timer);
			}
		}
	}
}
