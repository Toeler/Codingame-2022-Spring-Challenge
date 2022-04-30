using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class Monster : AbstractEntity {
		public int Health { get; }
		public bool IsTargetingBase { get; }
		public Target Target { get; }
		public override int MaxSpeed => InitialState.MonsterMoveSpeed;

		public Monster(int id, Vector position, int shieldLife, bool isControlled, int health, Vector vector, bool isTargetingBase, Target target) : base(id, position,
			shieldLife, isControlled) {
			Health = health;
			Vector = vector;
			IsTargetingBase = isTargetingBase;
			Target = target;
		}
	}
}
