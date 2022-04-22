using System;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class State {
		private static Vector _centerOfMap = new Vector(17630 / 2, 9000 / 2);

		private InitialState _initialState;
		public int MyHealth { get; }
		public int MyMana { get; }
		public int EnemyHealth { get; }
		public int EnemyMana { get; }

		public Hero[] MyHeroes { get; }
		public Hero[] EnemyHeroes { get; }
		public Monster[] Monsters { get; }

		public State(InitialState initialState, int myHealth, int myMana, int enemyHealth, int enemyMana, AbstractEntity[] entities) {
			_initialState = initialState;
			MyHealth = myHealth;
			MyMana = myMana;
			EnemyHealth = enemyHealth;
			EnemyMana = enemyMana;

			MyHeroes = entities.OfType<Hero>().Where(h => h.IsMine).ToArray();
			EnemyHeroes = entities.OfType<Hero>().Where(h => !h.IsMine).ToArray();
			Monsters = entities.OfType<Monster>().ToArray();
		}

		public IList<IEnumerable<Command>> GetPossibleCommands() {
			IList<Monster> monstersNearMyBase = Monsters
				.Where(m =>
					m.Position.DistanceTo(_initialState.MyBase) < 10000 ||
					m.Position.GetCollisionTime(m.Vector, _initialState.MyBase, 10000) > 0
				).ToList();
			IList<Command[]> actionsPerHero = MyHeroes.Select(hero => {
				if (monstersNearMyBase.Count > 0) {
					return monstersNearMyBase.Select(hero.MoveTo).Concat(new[] {new WaitCommand()}).ToArray();
				}

				double angle = _initialState.MyBase.GetAngleTo(_centerOfMap);
				return monstersNearMyBase.Select(hero.MoveTo).Concat(new[] {new WaitCommand()}).ToArray();
			}).ToList();
			return GetPossibleCommands(actionsPerHero);
		}

		private static IList<IEnumerable<Command>> GetPossibleCommands(IList<Command[]> commandsPerHero, int heroIndex = 0) {
			IList<IEnumerable<Command>> possibleMoves = new List<IEnumerable<Command>>();

			if (heroIndex == commandsPerHero.Count) {
				return possibleMoves;
			}

			foreach (Command command in commandsPerHero[heroIndex]) {
				IList<IEnumerable<Command>> possibleCommands = GetPossibleCommands(commandsPerHero, heroIndex + 1);
				if (possibleCommands.Count > 0) {
					foreach (IEnumerable<Command> setOfCommands in possibleCommands) {
						possibleMoves.Add(new[] {command}.Concat(setOfCommands));
					}
				} else {
					// This is the last hero
					possibleMoves.Add(new[] {command});
				}
			}

			return possibleMoves;
		}

		public double ScoreCommand(IEnumerable<Command> commandsPerHero) {
			double score = 0;

			foreach (Command command in commandsPerHero) {
				if (command is MoveCommand {Hero: { } hero, Entity: Monster monster}) {
					const int turnsToBeWorried = 100;
					double collisionTime = monster.Position.GetCollisionTime(monster.Vector, _initialState.MyBase, 300);
					double threatFactor = Math.Max(0, turnsToBeWorried - collisionTime) * 10;

					double distanceToHeroFactor = hero.Position.DistanceTo(monster.Position);

					return (collisionTime + distanceToHeroFactor) * -1;
				} else {
					return double.MinValue;
				}
			}

			return score;
		}
	}
}
