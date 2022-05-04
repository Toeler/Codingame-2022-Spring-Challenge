using System;
using System.Collections.Generic;
using System.Linq;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class State : InitialState {
		public int TurnNumber { get; }
		public int MyHealth { get; }
		public int MyMana { get; }
		public int EnemyHealth { get; }
		public int EnemyMana { get; }

		public IList<Hero> AllHeroes { get; }
		public Hero[] MyHeroes { get; }
		public Hero[] EnemyHeroes { get; }
		public Monster[] Monsters { get; }

		public IList<Monster> WanderingMonsters { get; }
		public IList<Monster> MonstersThreateningMyBase { get; }

		public IList<Vector> PatrolLocations {
			get {
				IList<Vector> locations = new List<Vector> {
					new Vector(1400, 7000),
					new Vector(7000, 1400),
					new Vector(5000, 5000),
					new Vector(8500, 5000),
					new Vector(10500, 1400),
					new Vector(5500, 8000)
				};

				return MyBase == Vector.Zero ? locations : locations.Select(location => MyBase - location).ToList();
			}
		}

		public State(InitialState initialState, int turnNumber, int myHealth, int myMana, int enemyHealth, int enemyMana, AbstractEntity[] entities): base(initialState) {
			TurnNumber = turnNumber;
			MyHealth = myHealth;
			MyMana = myMana;
			EnemyHealth = enemyHealth;
			EnemyMana = enemyMana;

			AllHeroes = entities.OfType<Hero>().ToList();
			MyHeroes = AllHeroes.Where(h => h.IsMine).ToArray();
			EnemyHeroes = AllHeroes.Where(h => !h.IsMine).ToArray();
			Monsters = entities.OfType<Monster>().ToArray();
			WanderingMonsters = Monsters.Where(m => m.Target == Target.Neither).ToList();
			MonstersThreateningMyBase = Monsters.Where(m => m.Target == Target.MyBase).OrderBy(m => m.GetCollisionTimeToBase(MyBase, FieldRadius, BaseRadius)).ToList();
		}
	}
}
