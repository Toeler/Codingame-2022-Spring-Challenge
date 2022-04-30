using Lib;

namespace Codingame_2022_Spring_Challenge.Commands {
	public class InterceptCommand : AbstractCommand {
		public override string CommandName => "MOVE";

		public InterceptCommand(Hero hero, AbstractEntity targetEntity, HeroRole role) : base(targetEntity, role) {
			var vectorToTarget = targetEntity.Position - hero.Position;
			var targetHeading = targetEntity.Vector.Normalize();
			var myHeading = hero.Vector.Normalize();

			Vector desiredPosition;
			if (vectorToTarget.DotMultiply(myHeading) > 0 && myHeading.DotMultiply(targetHeading) < -0.95) {
				desiredPosition = GetInterceptPosition(hero, targetEntity);
			} else {
				var lookAheadTime = vectorToTarget.Length / (hero.MaxSpeed + targetEntity.MaxSpeed);
				var expectedTargetPosition = targetEntity.Position + targetEntity.Vector * lookAheadTime;

				desiredPosition = GetInterceptPosition(hero, expectedTargetPosition);
			}

			TargetPosition = (hero.Position + desiredPosition).Truncate();
		}

		private static Vector GetInterceptPosition(AbstractEntity entity, AbstractEntity target) {
			return GetInterceptPosition(entity, target.Position);
		}
		private static Vector GetInterceptPosition(AbstractEntity entity, Vector target) {
			var desiredVelocity = target - entity.Position;
			return desiredVelocity.Truncate(entity.MaxSpeed);
		}
	}
}
