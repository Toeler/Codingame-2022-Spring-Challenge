using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public abstract class FarmMonstersCommand : AbstractCommand {
		public override string CommandName => "MOVE";

		protected FarmMonstersCommand(Vector target, HeroRole role) : base(target, role) {
		}
	}
}
