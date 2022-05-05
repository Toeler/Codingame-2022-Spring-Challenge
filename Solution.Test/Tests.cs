using System;
using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using NUnit.Framework;

namespace Codingame_2022_Spring_Challenge.Test {
	public class Tests {
		private const string TopLeftInitialState = "0 0|3";
		private const string BottomRightInitialState = "17630 9000|3";

		[SetUp]
		public void Setup() { }

		private IEnumerable<AbstractCommand> GetTurnResult(string initialInput, string previousTurn, string turnToTest) {
			var consoleReader = new DebugReader($"{initialInput}|{(!string.IsNullOrEmpty(previousTurn) ? previousTurn + "|" : "")}{turnToTest}");
			StateReader stateReader = new StateReader(consoleReader);
			Solver solver = new Solver();

			InitialState initialState = stateReader.ReadInitialState();
			consoleReader.Flush();

			State previousState = null;
			if (!string.IsNullOrEmpty(previousTurn)) {
				previousState = stateReader.ReadTurn(49, initialState, null);
			}

			var state = stateReader.ReadTurn(50, initialState, previousState);
			Timer timer = new Timer(TimeSpan.FromMilliseconds(500));
			consoleReader.Flush();
			IEnumerable<AbstractCommand> commands = solver.GetSolution(state, timer) as IEnumerable<AbstractCommand>;
			Console.WriteLine(string.Join(Environment.NewLine, commands.Select(command => $"{command} {command.GetType().Name} {command.Role}")));
			return commands;
		}

		private void TestTurn(string initialInput, string previousTurn, string turnToTest, string expectedOutput) {
			IEnumerable<AbstractCommand> commands = GetTurnResult(initialInput, previousTurn, turnToTest);
			Assert.AreEqual(expectedOutput, string.Join("|", commands));
		}

		[Test]
		public void WillNotCastWindOnEasyTarget() {
			var previousTurn =
				"3 60|3 52|8|0 1 3123 3817 11 0 -1 -1 -1 -1 -1|1 1 5859 3338 0 0 -1 -1 -1 -1 -1|2 1 6500 8021 0 0 -1 -1 -1 -1 -1|3 2 889 4886 0 0 -1 -1 -1 -1 -1|15 0 6690 3356 0 0 1 -125 -379 0 0|18 0 4903 1973 0 0 11 -326 231 0 1|23 0 6337 8497 0 0 10 -354 -186 0 0|25 0 6740 7769 0 0 8 275 -290 0 0";
			string input =
				"3 62|3 52|10|0 1 3838 3459 10 0 -1 -1 -1 -1 -1|1 1 5217 2862 0 0 -1 -1 -1 -1 -1|2 1 6819 7684 0 0 -1 -1 -1 -1 -1|3 2 1664 4687 0 0 -1 -1 -1 -1 -1|12 0 8974 7550 0 0 10 -167 363 0 0|15 0 6565 2977 0 0 1 -125 -379 0 0|18 0 4577 2204 0 0 11 -326 231 0 1|23 0 5983 8311 0 0 10 -354 -186 0 0|25 0 7015 7479 0 0 6 275 -290 0 0|27 0 7969 8950 0 0 12 -282 -283 0 1";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.None.Contains("WIND"));
		}

		[Test]
		public void WillNotCastWindOnEasyTargetOnFieldBorder() {
			var previousTurn =
				"3 58|3 36|9|0 1 6291 5944 7 0 -1 -1 -1 -1 -1|1 1 5417 2305 3 0 -1 -1 -1 -1 -1|2 1 7362 6583 0 0 -1 -1 -1 -1 -1|3 2 6255 2717 0 0 -1 -1 -1 -1 -1|20 0 4161 1954 0 0 3 -362 -170 1 1|24 0 7040 5291 0 0 12 -275 290 0 0|33 0 7543 6356 0 0 5 248 -313 0 0|35 0 7891 7591 0 0 13 -154 -368 0 1|37 0 5451 7489 0 0 13 106 -385 0 0";
			string input =
				"3 64|3 40|10|0 1 6939 6412 6 0 -1 -1 -1 -1 -1|1 1 4657 2058 2 0 -1 -1 -1 -1 -1|2 1 7603 6280 0 0 -1 -1 -1 -1 -1|3 2 5532 2377 0 0 -1 -1 -1 -1 -1|20 0 3799 1784 0 0 1 -362 -170 1 1|23 0 1027 5707 0 0 10 -354 -186 0 0|24 0 6765 5581 0 0 12 -275 290 0 0|33 0 7791 6043 0 0 1 248 -313 0 0|35 0 7737 7223 0 0 13 -154 -368 0 1|37 0 5557 7104 0 0 13 106 -385 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.None.Contains("WIND"));
		}

		[Test]
		public void ShouldNotShieldMyselfWhenNoMonstersNearby() {
			var previousTurn =
				"3 60|3 40|6|0 1 2617 4283 0 0 -1 -1 -1 -1 -1|1 1 7141 4726 0 0 -1 -1 -1 -1 -1|2 1 6549 6864 0 0 -1 -1 -1 -1 -1|3 2 2079 4530 0 0 -1 -1 -1 -1 -1|15 0 7065 4493 0 0 5 -125 -379 0 0|25 0 5915 8639 0 0 12 275 -290 0 0";
			string input =
				"3 62|3 44|8|0 1 1899 4633 0 0 -1 -1 -1 -1 -1|1 1 7039 4415 0 0 -1 -1 -1 -1 -1|2 1 6427 7654 0 0 -1 -1 -1 -1 -1|3 2 1319 4776 0 0 -1 -1 -1 -1 -1|15 0 6940 4114 0 0 3 -125 -379 0 0|18 0 5555 1511 0 0 11 -326 231 0 1|23 0 7045 8869 0 0 12 -354 -186 0 0|25 0 6190 8349 0 0 12 275 -290 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.None.Contains("SHIELD"));
		}

		[Test]
		public void Hero1ShouldNotExplore() {
			var previousTurn =
				"3 94|3 32|6|0 1 5803 3639 0 0 -1 -1 -1 -1 -1|1 1 1805 824 7 0 -1 -1 -1 -1 -1|2 1 5642 6614 0 0 -1 -1 -1 -1 -1|3 2 4116 1510 0 0 -1 -1 -1 -1 -1|20 0 1057 348 0 0 11 -379 -125 1 1|33 0 6551 7608 0 0 13 248 -313 0 0";
			string input =
				"3 86|3 36|7|0 1 5397 4329 0 0 -1 -1 -1 -1 -1|1 1 1805 824 6 0 -1 -1 -1 -1 -1|2 1 6345 6994 0 0 -1 -1 -1 -1 -1|3 2 4266 2296 0 0 -1 -1 -1 -1 -1|20 0 3059 1262 0 0 11 -369 -152 1 1|33 0 6799 7295 0 0 11 248 -313 0 0|37 0 5133 8644 0 0 13 106 -385 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.ElementAt(1).ToString(), Does.Not.Contain("Explorer"));
		}

		[Test]
		public void SomebodyShouldDefendFromEnemyHero() {
			var previousTurn =
				"3 22|3 2|8|0 1 6356 6448 0 0 -1 -1 -1 -1 -1|1 1 8659 3312 0 0 -1 -1 -1 -1 -1|2 1 5244 5896 0 0 -1 -1 -1 -1 -1|3 2 5097 2178 0 0 -1 -1 -1 -1 -1|8 0 8555 3416 0 0 8 -284 281 0 0|11 0 5695 7309 0 0 10 -312 -249 0 1|13 0 6485 6169 0 0 2 167 -363 0 0|17 0 5395 7889 0 0 11 116 -382 0 0";
			string input =
				"3 28|3 4|9|0 1 6527 6076 0 0 -1 -1 -1 -1 -1|1 1 8520 3450 0 0 -1 -1 -1 -1 -1|2 1 5440 6671 0 0 -1 -1 -1 -1 -1|3 2 4374 1838 0 0 -1 -1 -1 -1 -1|8 0 8271 3697 0 0 6 -284 281 0 0|9 0 9359 5303 0 0 10 284 -281 0 0|11 0 5383 7060 0 0 8 -312 -249 0 1|15 0 8065 7525 0 0 11 -125 -379 0 0|17 0 5511 7507 0 0 11 116 -382 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.Some.Contains("Defender"));
		}

		[Test]
		public void Hero2ShouldDefendAgainstMonster() {
			var previousTurn =
				"3 56|3 22|6|0 1 6170 5917 0 0 -1 -1 -1 -1 -1|1 1 7161 4784 0 0 -1 -1 -1 -1 -1|2 1 3032 3749 0 0 -1 -1 -1 -1 -1|3 2 2520 4050 0 0 -1 -1 -1 -1 -1|11 0 2887 5068 0 0 6 -312 -249 0 1|15 0 7065 4493 0 0 3 -125 -379 0 0";
			string input =
				"3 58|3 22|8|0 1 6105 6714 0 0 -1 -1 -1 -1 -1|1 1 7033 4396 0 0 -1 -1 -1 -1 -1|2 1 2356 4176 0 0 -1 -1 -1 -1 -1|3 2 1819 4435 0 0 -1 -1 -1 -1 -1|11 0 2575 4819 0 0 6 -312 -249 0 1|15 0 6940 4114 0 0 1 -125 -379 0 0|18 0 5555 1511 0 0 11 -326 231 0 1|25 0 6190 8349 0 0 12 275 -290 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.ElementAt(2).ToString(), Does.Contain("Defender"));
		}

		[Test]
		public void Hero0ShouldNotBeDefending() {
			var previousTurn =
				"3 86|3 22|6|0 1 6783 3639 0 0 -1 -1 -1 -1 -1|1 1 4757 1924 0 0 -1 -1 -1 -1 -1|2 1 6500 8021 0 0 -1 -1 -1 -1 -1|3 2 934 4925 0 0 -1 -1 -1 -1 -1|23 0 6337 8497 0 0 10 -354 -186 0 0|25 0 6740 7769 0 0 8 275 -290 0 0";
			string input =
				"3 88|3 24|8|0 1 6148 4125 0 0 -1 -1 -1 -1 -1|1 1 4232 2527 0 0 -1 -1 -1 -1 -1|2 1 6819 7684 0 0 -1 -1 -1 -1 -1|3 2 785 4140 0 0 -1 -1 -1 -1 -1|12 0 8974 7550 0 0 10 -167 363 0 0|23 0 5983 8311 0 0 10 -354 -186 0 0|25 0 7015 7479 0 0 6 275 -290 0 0|27 0 7969 8950 0 0 12 -282 -283 0 1";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.ElementAt(0).ToString(), Does.Not.Contain("Defender"));
		}

		[Test]
		public void NobodyShouldHaveNothingToDo() {
			var previousTurn =
				"1 204|3 120|8|0 1 6058 7728 0 0 -1 -1 -1 -1 -1|1 1 6945 4323 2 0 -1 -1 -1 -1 -1|2 1 4373 995 8 0 -1 -1 -1 -1 -1|3 2 3902 2028 0 0 -1 -1 -1 -1 -1|48 0 5871 7965 0 0 5 -248 313 0 0|58 0 2322 523 0 0 8 -390 -87 1 1|63 0 6719 7383 0 0 17 -262 -302 0 1|65 0 7879 8879 0 0 17 383 -115 0 2";
			string input =
				"1 206|3 122|8|0 1 5808 8043 0 0 -1 -1 -1 -1 -1|1 1 7679 4642 1 0 -1 -1 -1 -1 -1|2 1 3594 817 7 0 -1 -1 -1 -1 -1|3 2 3320 1481 0 0 -1 -1 -1 -1 -1|48 0 5623 8278 0 0 3 -248 313 0 0|58 0 1932 436 0 0 8 -390 -88 1 1|63 0 6457 7081 0 0 17 -262 -302 0 1|69 0 6083 8827 0 0 17 317 -243 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.None.Contains("WAIT"));
		}

		[Test]
		public void MovePickingShouldNotGetStuck() {
			var previousTurn =
				"3 132|3 64|7|0 1 3997 6237 0 0 -1 -1 -1 -1 -1|1 1 3624 838 7 0 -1 -1 -1 -1 -1|2 1 2292 2432 3 0 -1 -1 -1 -1 -1|3 2 3683 1616 0 0 -1 -1 -1 -1 -1|28 0 3767 6422 0 0 4 -312 249 0 0|38 0 3663 797 0 0 12 -390 -85 1 1|41 0 4605 4213 0 0 10 -15 -399 0 1";
			string input =
				"3 136|3 58|7|0 1 3690 6483 0 0 -1 -1 -1 -1 -1|1 1 3644 792 6 0 -1 -1 -1 -1 -1|2 1 3045 2700 2 0 -1 -1 -1 -1 -1|3 2 3683 1616 0 0 -1 -1 -1 -1 -1|28 0 3455 6671 0 0 2 -312 249 0 0|38 0 1514 330 0 0 10 -390 -85 1 1|41 0 4590 3814 0 0 10 -15 -399 0 1";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.None.Contains("WAIT"));
		}
	}
}
