using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class MoveCommand: Command<MoveCommand> {
		private readonly Vector _to;
		public Hero Hero { get; }
		public AbstractEntity Entity { get; }

		public MoveCommand(Vector to) {
			_to = to;
		}

		public MoveCommand(Hero hero, AbstractEntity entity): this(entity.Position) {
			Hero = hero;
			Entity = entity;
		}

		public override string ToString() {
			return $"MOVE {_to}";
		}
	}
}
