using System;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public abstract class AbstractEntity {
		public int Id { get; }
		public Vector Position { get; }
		public int ShieldLife { get; }
		public bool IsShielded => ShieldLife > 0;
		public bool IsControlled { get; }
		public Vector Vector { get; protected set; }
		public abstract int MaxSpeed { get; }

		public AbstractEntity(int id, Vector position, int shieldLife, bool isControlled) {
		    Id = id;
		    Position = position;
		    ShieldLife = shieldLife;
		    IsControlled = isControlled;
		}

		public double GetCollisionTime(Vector target, double radius) {
			return Position.GetCollisionTime(Vector, target, radius);
		}

		public double GetCollisionTimeToBase(Vector targetBase, double fieldRadius, double baseRadius) {
			double collisionWithField = Math.Ceiling(GetCollisionTime(targetBase, fieldRadius));
			if (collisionWithField == 0) {
				return Math.Ceiling(GetCollisionTime(targetBase, baseRadius));
			}

			if (double.IsPositiveInfinity(collisionWithField)) {
				return double.PositiveInfinity;
			}

			Vector positionWhenTargetingBase = Position + (Vector * (int)collisionWithField);
			double distanceToBase = positionWhenTargetingBase.DistanceTo(targetBase);
			int distanceToStep = (int)Math.Min(distanceToBase, InitialState.MonsterMoveSpeed);
			Vector newSpeed = ((targetBase - positionWhenTargetingBase).Normalize() * distanceToStep).Truncate();
			return collisionWithField + Math.Ceiling(positionWhenTargetingBase.GetCollisionTime(newSpeed, targetBase, baseRadius));
		}
	}
}
