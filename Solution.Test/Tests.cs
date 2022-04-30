using System;
using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using NUnit.Framework;

namespace Codingame_2022_Spring_Challenge.Test {
	public class Tests {
		[SetUp]
		public void Setup() { }

		private void TestTurn(string initialInput, string previousTurn, string turnToTest, string expectedOutput) {
			var consoleReader = new DebugReader($"{initialInput}|{(!string.IsNullOrEmpty(previousTurn) ? previousTurn + "|" : "")}{turnToTest}");
			StateReader stateReader = new StateReader(consoleReader);
			Solver solver = new Solver();

			InitialState initialState = stateReader.ReadInitialState();
			consoleReader.Flush();

			State previousState = null;
			if (!string.IsNullOrEmpty(previousTurn)) {
				previousState = stateReader.ReadTurn(initialState, null);
			}

			var state = stateReader.ReadTurn(initialState, previousState);
			Timer timer = new Timer(TimeSpan.FromMilliseconds(5000));
			consoleReader.Flush();
			IEnumerable<AbstractCommand> commands = solver.GetSolution(state, timer) as IEnumerable<AbstractCommand>;
			Console.WriteLine(string.Join(Environment.NewLine, commands.Select(command => $"{command} {command.GetType().Name} {command.Role}")));
			Assert.AreEqual(expectedOutput, string.Join("|", commands));
		}

		[TestCase("",
			"3 0|3 0|3|0 1 1131 1131 0 0 -1 -1 -1 -1 -1|1 1 1414 849 0 0 -1 -1 -1 -1 -1|2 1 849 1414 0 0 -1 -1 -1 -1 -1",
			"MOVE 5000 5000 Explorer|MOVE 7000 1400 Explorer|MOVE 6000 8000 Attacker")]
		[TestCase(
			"3 2|3 0|4|0 1 5000 5000 0 0 -1 -1 -1 -1 -1|1 1 7796 1352 0 0 -1 -1 -1 -1 -1|2 1 1400 7000 0 0 -1 -1 -1 -1 -1|6 0 8080 1896 0 0 8 -105 385 0 0",
			"3 4|3 4|5|0 1 5000 5000 0 0 -1 -1 -1 -1 -1|1 1 8596 1367 0 0 -1 -1 -1 -1 -1|2 1 1400 7000 0 0 -1 -1 -1 -1 -1|6 0 7975 2281 0 0 6 -105 385 0 0|9 0 4015 6703 0 0 10 -100 -387 0 1",
			"MOVE 4414 5544 Attacker|MOVE 8202 2063 Farmer|MOVE 1400 7000 Explorer")]
		[TestCase(
			"3 16|3 4|11|0 1 5800 5000 0 0 -1 -1 -1 -1 -1|1 1 7393 1532 0 0 -1 -1 -1 -1 -1|2 1 1814 4417 0 0 -1 -1 -1 -1 -1|19 0 5383 847 0 0 11 -143 -373 0 0|20 0 6431 6353 0 0 11 -266 298 0 0|24 0 8180 2813 0 0 8 275 289 0 2|25 0 118 4088 0 0 12 -11 -399 1 1|27 0 7373 4395 0 0 12 -103 -386 0 0|28 0 6287 2014 0 0 4 -333 221 0 0|30 0 7546 2567 0 0 11 -141 374 0 0|33 0 5886 6370 0 0 13 119 -381 0 0",
			"3 16|3 6|11|0 1 5072 5331 0 0 -1 -1 -1 -1 -1|1 1 8193 1499 0 0 -1 -1 -1 -1 -1|2 1 1107 4044 0 0 -1 -1 -1 -1 -1|19 0 5240 474 0 0 11 -143 -373 0 0|20 0 6165 6651 0 0 11 -266 298 0 0|24 0 8455 3102 0 0 8 275 289 0 2|25 0 107 3689 0 0 12 -11 -399 1 1|30 0 7405 2941 0 0 11 -141 374 0 0|32 0 9560 2360 0 0 9 -119 381 0 0|33 0 6005 5989 0 0 13 119 -381 0 0|34 0 9940 851 0 0 13 225 330 0 2",
			"MOVE 5841 5548 Attacker|MOVE 7395 1440 Defender|SPELL WIND 1444 5278 Defender")]
		[TestCase(
			"3 60|3 4|7|0 1 6559 4652 0 0 -1 -1 -1 -1 -1|1 1 6326 970 0 0 -1 -1 -1 -1 -1|2 1 2616 5940 0 0 -1 -1 -1 -1 -1|15 0 2374 5790 0 0 5 -339 -211 0 1|19 0 6813 4577 0 0 9 -143 -373 0 0|22 0 5350 155 0 0 12 -385 106 0 1|25 0 2340 7198 0 0 12 -275 -289 0 1",
			"3 66|3 8|8|0 1 6781 4494 0 0 -1 -1 -1 -1 -1|1 1 5613 608 0 0 -1 -1 -1 -1 -1|2 1 2293 5739 0 0 -1 -1 -1 -1 -1|15 0 2035 5579 0 0 3 -339 -211 0 1|19 0 6670 4204 0 0 7 -143 -373 0 0|21 0 8805 5329 0 0 11 266 -298 0 0|22 0 4965 261 0 0 10 -399 -20 1 1|25 0 2065 6909 0 0 12 -275 -289 0 1",
			"MOVE 6632 4107 Attacker|SPELL WIND 6885 745 Defender|MOVE 2384 4944 Defender")]
		[TestCase(
			"",
			"3 0|3 0|3|0 1 3961 3961 0 0 -1 -1 -1 -1 -1|1 1 5399 1244 0 0 -1 -1 -1 -1 -1|2 1 3315 4568 0 0 -1 -1 -1 -1 -1",
			"MOVE 5000 5000 Explorer|MOVE 7000 1400 Explorer|MOVE 6000 8000 Attacker")]
		[TestCase(
			"",
			"3 174|3 70|8|0 1 5528 1627 0 0 -1 -1 -1 -1 -1|1 1 9475 7796 0 0 -1 -1 -1 -1 -1|2 1 667 5346 0 0 -1 -1 -1 -1 -1|24 0 10237 5969 0 0 4 289 276 0 2|26 0 8597 7839 0 0 8 123 380 0 0|37 0 5865 1483 0 0 7 50 -396 0 0|42 0 5284 1808 0 0 6 -321 237 0 1|47 0 9565 7525 0 0 9 125 -379 0 0",
			"MOVE 5202 1868 Defender|SPELL CONTROL 47 17630 9000 Exterminator|MOVE 6000 8000 Attacker Attacker")]
		public void TestTopLeftBaseTurn(string previousTurn, string turnToTest, string expectedOutput) {
			TestTurn("0 0|3", previousTurn, turnToTest, expectedOutput);
		}

		[TestCase(
			"3 480|3 216|5|3 1 10631 7766 0 0 -1 -1 -1 -1 -1|4 1 15431 5450 0 0 -1 -1 -1 -1 -1|5 1 12642 5419 0 0 -1 -1 -1 -1 -1|168 0 16144 5107 0 0 30 142 373 1 1|173 0 9171 7795 0 0 30 363 -167 0 1",
			"3 474|3 216|4|3 1 9831 7728 0 0 -1 -1 -1 -1 -1|4 1 15431 5450 0 0 -1 -1 -1 -1 -1|5 1 13426 5575 0 0 -1 -1 -1 -1 -1|173 0 9534 7628 0 0 28 363 -167 0 1",
				"MOVE 9628 7584 Defender|MOVE 12630 4000 Explorer|MOVE 11630 1000 Attacker")]
		public void TestBottomRightBaseTurn(string previousTurn, string turnToTest, string expectedOutput) {
			TestTurn("17630 9000|3", previousTurn, turnToTest, expectedOutput);
		}
	}
}
