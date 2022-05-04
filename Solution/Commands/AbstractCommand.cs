#nullable enable
using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public abstract class AbstractCommand : Command {
		public abstract string CommandName { get; }
		private AbstractEntity? _targetEntity;
		public AbstractEntity? TargetEntity {
			get {
				return _targetEntity;
			}
			protected set {
				Target = value?.Position.ToString();
				_targetEntity = value;
			}
		}
		public HeroRole Role { get; }

		protected string? Target { get; set; }

		private Vector? _targetPosition;
		public Vector? TargetPosition {
			get {
				return _targetPosition;
			}
			protected set {
				Target = value?.ToString();
				_targetPosition = value;
			}
		}

		protected AbstractCommand(HeroRole role) {
			Role = role;
		}

		protected AbstractCommand(AbstractEntity targetEntity, HeroRole role) : this(role) {
			TargetEntity = targetEntity;
		}

		protected AbstractCommand(Vector targetPosition, HeroRole role) : this(role) {
			TargetPosition = targetPosition;
		}

		public override string ToString() {
			string role = string.Empty;
			role = $" {Role}";
			return $"{CommandName} {Target}{role}";
		}
	}
}
