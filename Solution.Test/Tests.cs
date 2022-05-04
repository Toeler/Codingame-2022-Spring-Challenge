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

		[TestCase("",
			"3 0|3 0|3|0 1 1131 1131 0 0 -1 -1 -1 -1 -1|1 1 1414 849 0 0 -1 -1 -1 -1 -1|2 1 849 1414 0 0 -1 -1 -1 -1 -1",
			"MOVE 5000 5000 Explorer|MOVE 7000 1400 Explorer|MOVE 6000 8000 Attacker")]
		[TestCase(
			"3 2|3 0|4|0 1 5000 5000 0 0 -1 -1 -1 -1 -1|1 1 7796 1352 0 0 -1 -1 -1 -1 -1|2 1 1400 7000 0 0 -1 -1 -1 -1 -1|6 0 8080 1896 0 0 8 -105 385 0 0",
			"3 4|3 4|5|0 1 5000 5000 0 0 -1 -1 -1 -1 -1|1 1 8596 1367 0 0 -1 -1 -1 -1 -1|2 1 1400 7000 0 0 -1 -1 -1 -1 -1|6 0 7975 2281 0 0 6 -105 385 0 0|9 0 4015 6703 0 0 10 -100 -387 0 1",
			"MOVE 4414 5544 Attacker|MOVE 8202 2063 Farmer|MOVE 1400 7000 Explorer")]
		[TestCase(
			"",
			"3 0|3 0|3|0 1 3961 3961 0 0 -1 -1 -1 -1 -1|1 1 5399 1244 0 0 -1 -1 -1 -1 -1|2 1 3315 4568 0 0 -1 -1 -1 -1 -1",
			"MOVE 5000 5000 Explorer|MOVE 7000 1400 Explorer|MOVE 6000 8000 Attacker")]
		public void TestTopLeftBaseTurn(string previousTurn, string turnToTest, string expectedOutput) {
			TestTurn(TopLeftInitialState, previousTurn, turnToTest, expectedOutput);
		}

		[TestCase(
			"3 480|3 216|5|3 1 10631 7766 0 0 -1 -1 -1 -1 -1|4 1 15431 5450 0 0 -1 -1 -1 -1 -1|5 1 12642 5419 0 0 -1 -1 -1 -1 -1|168 0 16144 5107 0 0 30 142 373 1 1|173 0 9171 7795 0 0 30 363 -167 0 1",
			"3 474|3 216|4|3 1 9831 7728 0 0 -1 -1 -1 -1 -1|4 1 15431 5450 0 0 -1 -1 -1 -1 -1|5 1 13426 5575 0 0 -1 -1 -1 -1 -1|173 0 9534 7628 0 0 28 363 -167 0 1",
				"MOVE 9628 7584 Defender|MOVE 12630 4000 Explorer|MOVE 11630 1000 Attacker")]
		public void TestBottomRightBaseTurn(string previousTurn, string turnToTest, string expectedOutput) {
			TestTurn(BottomRightInitialState, previousTurn, turnToTest, expectedOutput);
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
		public void WillCastWindOnMonsterThatEnemyCanWindIntoMyBase() {
			var previousTurn =
				"3 104|3 48|10|0 1 3133 1044 12 0 -1 -1 -1 -1 -1|1 1 1788 1790 1 0 -1 -1 -1 -1 -1|2 1 5937 6694 0 0 -1 -1 -1 -1 -1|3 2 2654 2498 0 0 -1 -1 -1 -1 -1|63 0 3256 437 0 0 9 -396 -53 1 1|67 0 314 221 0 0 5 -313 -220 1 1|70 0 3153 4255 0 0 18 -298 266 0 0|73 0 98 1874 0 0 18 -20 -399 1 1|77 0 1679 5165 0 0 18 -224 -331 0 1|81 0 6039 6415 0 0 9 136 -376 0 0";
			string input =
				"2 108|3 52|9|0 1 3051 409 11 0 -1 -1 -1 -1 -1|1 1 1018 1576 0 0 -1 -1 -1 -1 -1|2 1 6072 6321 0 0 -1 -1 -1 -1 -1|3 2 2901 1738 0 0 -1 -1 -1 -1 -1|63 0 2860 384 0 0 7 -396 -53 1 1|70 0 2855 4521 0 0 18 -298 266 0 0|73 0 78 1475 0 0 18 -21 -399 1 1|77 0 1455 4834 0 0 18 -224 -331 0 1|81 0 6175 6039 0 0 7 136 -376 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.ElementAt(0).ToString(), Does.Contain("WIND"));
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

		[Test]
		public void ShouldWindIntoEnemyBase() {
			var previousTurn =
				"3 38|3 52|9|0 1 8488 4583 0 0 -1 -1 -1 -1 -1|1 1 527 1711 0 0 -1 -1 -1 -1 -1|2 1 13363 7587 0 0 -1 -1 -1 -1 -1|3 2 1746 5167 0 0 -1 -1 -1 -1 -1|26 0 12458 7042 0 0 12 374 141 0 2|28 0 13519 7136 0 0 10 364 165 1 2|31 0 11732 8605 0 0 13 399 25 0 2|33 0 726 2356 0 0 7 -117 -382 1 1|35 0 11778 8579 0 0 13 398 28 0 2";
			string input =
				"3 32|3 52|8|0 1 8266 5351 0 0 -1 -1 -1 -1 -1|1 1 660 2141 0 0 -1 -1 -1 -1 -1|2 1 13363 7587 0 0 -1 -1 -1 -1 -1|3 2 1500 4407 0 0 -1 -1 -1 -1 -1|26 0 14546 7733 0 0 12 369 152 1 2|31 0 12131 8630 0 0 13 399 25 0 2|33 0 609 1974 0 0 5 -117 -382 1 1|35 0 12176 8607 0 0 13 398 28 0 2";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.Some.Contains("WIND"));
		}

		[Test]
		public void ShouldUseWindToProtectMyBase() {
			var previousTurn =
				"3 86|3 54|11|0 1 7536 7776 0 0 -1 -1 -1 -1 -1|1 1 2744 793 7 0 -1 -1 -1 -1 -1|2 1 7536 7776 0 0 -1 -1 -1 -1 -1|3 2 2574 2660 0 0 -1 -1 -1 -1 -1|35 0 1085 314 0 1 9 384 110 1 1|38 0 5493 7341 0 0 14 -151 370 0 0|41 0 1705 1823 0 0 14 -273 -292 1 1|42 0 1270 61 0 0 14 -399 -19 1 1|49 0 6886 6577 0 0 15 390 87 0 2|51 0 7380 7398 0 0 7 -205 -343 0 1|53 0 5961 8275 0 0 15 399 24 0 2";
			string input =
				"2 70|3 48|11|0 1 7536 7776 0 0 -1 -1 -1 -1 -1|1 1 1976 571 6 0 -1 -1 -1 -1 -1|2 1 7536 7776 0 0 -1 -1 -1 -1 -1|3 2 2574 2660 0 0 -1 -1 -1 -1 -1|35 0 1470 425 0 0 9 -384 -111 1 1|38 0 5342 7711 0 1 14 397 41 0 2|42 0 871 42 0 0 14 -399 -19 1 1|49 0 7276 6664 0 0 15 390 87 0 2|51 0 7175 7055 0 1 3 393 73 0 2|53 0 6360 8299 0 0 15 399 24 0 2|55 0 8812 8602 0 0 16 -1 -399 0 0";
			var commands = GetTurnResult(TopLeftInitialState, previousTurn, input);
			Assert.That(commands.Select(command => command.ToString()), Has.Some.Contains("WIND"));
		}
	}
}
