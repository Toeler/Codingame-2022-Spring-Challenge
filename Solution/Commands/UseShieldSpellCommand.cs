using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public class UseShieldSpellCommand : UseSpellCommand {
		public override string SpellName => "SHIELD";

		public UseShieldSpellCommand(AbstractEntity targetEntity, HeroRole role) : base(targetEntity, role) {
			Target = targetEntity.Id.ToString();
		}
	}
}
