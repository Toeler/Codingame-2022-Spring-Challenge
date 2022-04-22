using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class Hero : AbstractEntity {
		public bool IsMine { get; }

		public Hero(int id, Vector position, int shieldLife, bool isControlled, bool isMine) : base(id, position, shieldLife, isControlled) {
			IsMine = isMine;
		}

		public Command MoveTo(AbstractEntity entity) {
			return new MoveCommand(this, entity);
		}
	}
}
