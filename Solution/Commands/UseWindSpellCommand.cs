using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public class UseWindSpellCommand : UseSpellCommand {
		public override string SpellName => "WIND";
		public UseWindSpellCommand(Vector targetPosition, HeroRole role) : base(targetPosition, role) {
		}
	}
}
