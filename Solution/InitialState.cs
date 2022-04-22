using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class InitialState {
		public Vector MyBase { get; }
		public Vector EnemyBase { get; }
		public int HeroesPerPlayer { get; }

		public InitialState(Vector basePos, int heroesPerPlayer) {
			MyBase = basePos;
			EnemyBase = basePos;
			HeroesPerPlayer = heroesPerPlayer;
		}
	}
}
