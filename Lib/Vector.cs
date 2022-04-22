using System;
using System.Globalization;

namespace Lib {
	public class Vector {
		public static readonly Vector Zero = new Vector(0, 0);

		public int X { get; }
		public int Y { get; }

		public Vector(int x, int y) {
			X = x;
			Y = y;
		}

		public bool Equals(Vector other) {
			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}

			if (ReferenceEquals(this, obj)) {
				return true;
			}

			if (obj.GetType() != GetType()) {
				return false;
			}

			return Equals((Vector)obj);
		}

		public override int GetHashCode() {
			unchecked {
				return (X.GetHashCode() * 397) ^ Y.GetHashCode();
			}
		}

		public long Len2 => (long)X * X + (long)Y * Y;

		public static bool operator ==(Vector left, Vector right) => Equals(left, right);
		public static bool operator !=(Vector left, Vector right) => !Equals(left, right);

		public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
		public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
		public static Vector operator -(Vector a) => new Vector(-a.X, -a.Y);
		public static Vector operator *(Vector a, int k) => new Vector(k * a.X, k * a.Y);
		public static Vector operator *(int k, Vector a) => new Vector(k * a.X, k * a.Y);
		public static Vector operator /(Vector a, int k) => new Vector(a.X / k, a.Y / k);

		public long Distance2To(Vector point) => (this - point).Len2;

		public double DistanceTo(Vector b) => Math.Sqrt(Distance2To(b));

		public double GetCollisionTime(Vector speed, Vector target, double radius) {
			if (DistanceTo(target) <= radius) {
				return 0.0;
			}

			if (speed.Equals(Zero)) {
				return double.PositiveInfinity;
			}

			int x2 = X - target.X;
			int y2 = Y - target.Y;
			int vx = speed.X;
			int vy = speed.Y;

			int a = vx * vx + vy * vy;
			double b = 2.0 * (x2 * vx + y2 * vy);
			double c = x2 * x2 + y2 * y2 - radius * radius;
			double d = b * b - 4.0 * a * c;

			if (d < 0.0) {
				return double.PositiveInfinity;
			}

			double t = (-b - Math.Sqrt(d)) / (2.0 * a);
			return t <= 0.0 ? double.PositiveInfinity : t;
		}

		public double GetAngleTo(Vector p2)
		{
			(int x, int y) = p2;
			return Math.Atan2(y-Y, x-X);
		}

		public void Deconstruct(out int x, out int y)
		{
			x = X;
			y = Y;
		}

		public override string ToString() {
			return $"{X.ToString(CultureInfo.InvariantCulture)} {Y.ToString(CultureInfo.InvariantCulture)}";
		}
	}
}
