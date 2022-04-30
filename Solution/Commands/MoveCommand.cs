using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public class MoveCommand : AbstractCommand {
		public override string CommandName => "MOVE";

		public MoveCommand(Vector location, HeroRole role) : base(location, role) {
		}
	}
}
