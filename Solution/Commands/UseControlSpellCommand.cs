using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public class UseControlSpellCommand : UseSpellCommand {
		public override string SpellName => $"CONTROL {TargetEntity!.Id}";

		public UseControlSpellCommand(AbstractEntity targetEntity, Vector desiredPosition, HeroRole role) : base(targetEntity, role) {
			TargetPosition = desiredPosition;
		}
	}
}
