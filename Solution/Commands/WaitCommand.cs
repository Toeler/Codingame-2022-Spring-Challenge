using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public class WaitCommand : AbstractCommand {
		public override string CommandName => "WAIT";

		public WaitCommand(HeroRole role) : base(role) {
		}
	}
}
