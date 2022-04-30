using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public abstract class UseSpellCommand : AbstractCommand {
		public override string CommandName => $"SPELL {SpellName}";
		public abstract string SpellName { get; }

		protected UseSpellCommand(AbstractEntity targetEntity, HeroRole role) : base(targetEntity, role) {
		}

		protected UseSpellCommand(Vector targetPosition, HeroRole role) : base(targetPosition, role) {
		}
	}
}
