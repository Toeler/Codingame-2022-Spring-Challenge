using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class InitialState {
		public const int MapWidth = 17630;
		public const int MapHeight = 9000;
		public static Vector TopLeft = new Vector(0, 0);
		public static Vector BottomRight = new Vector(MapWidth, MapHeight);
		public const int FieldRadius = 5000;
		public const int BaseRadius = 300;
		public const int MonsterMoveSpeed = 400;
		public const int HeroVisionRange = 2200;
		public const int MonsterBaseDetectionRange = 7000;
		public const int SpellManaCost = 10;
		public const int WindSpellRange = 1280;
		public const int WindSpellAttackRange = 2200;
		public const int ControlSpellRange = 2200;
		public const int ShieldSpellRange = 2200;
		public const int MaximumDefenders = 2;
		public const int HeroMoveSpeed = 800;
		public const int FarmingRange = 10000;
		public const int ShieldTime = 12;
		public const int HeroAttackRadius = 800;
		public const int HeroDamagePerTurn = 2;

		public Vector MyBase { get; }
		public Vector EnemyBase { get; }
		public int HeroesPerPlayer { get; }

		public InitialState(Vector basePos, int heroesPerPlayer) {
			MyBase = basePos;
			EnemyBase = basePos == Vector.Zero ? new Vector(MapWidth, MapHeight) : Vector.Zero;
			HeroesPerPlayer = heroesPerPlayer;
		}

		public InitialState(InitialState initialState) : this(initialState.MyBase, initialState.HeroesPerPlayer) {
		}
	}
}
