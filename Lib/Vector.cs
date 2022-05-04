using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lib {
	public class Vector {
		public static readonly Vector Zero = new Vector(0, 0);

		public double X { get; }
		public double Y { get; }

		public Vector(double x, double y) {
			X = x;
			Y = y;
		}

		public Vector(Vector a, Vector b) {
			X = b.X - a.X;
			Y = b.Y - a.Y;
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

		public long Len2 => (long)Math.Pow(X, 2) + (long)Math.Pow(Y, 2);
		public double Length => Math.Sqrt(Len2);

		public static bool operator ==(Vector left, Vector right) => Equals(left, right);
		public static bool operator !=(Vector left, Vector right) => !Equals(left, right);

		public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
		public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
		public static Vector operator -(Vector a) => new Vector(-a.X, -a.Y);
		public static Vector operator *(Vector a, int k) => new Vector(k * a.X, k * a.Y);
		public static Vector operator *(Vector a, double k) => new Vector(k * a.X, k * a.Y);
		public static Vector operator *(int k, Vector a) => new Vector(k * a.X, k * a.Y);
		public static Vector operator *(double k, Vector a) => new Vector(k * a.X, k * a.Y);
		public static Vector operator /(Vector a, int k) => new Vector(a.X / k, a.Y / k);
		public static Vector operator /(Vector a, double k) => new Vector(a.X / k, a.Y / k);

		public long Distance2To(Vector point) => (this - point).Len2;

		public double DistanceTo(Vector b) => Math.Sqrt(Distance2To(b));

		public double GetCollisionTime(Vector speed, Vector target, double radius) {
			if (DistanceTo(target) <= radius) {
				return 0.0;
			}

			if (speed.Equals(Zero)) {
				return double.PositiveInfinity;
			}

			double x2 = X - target.X;
			double y2 = Y - target.Y;
			double vx = speed.X;
			double vy = speed.Y;

			double a = vx * vx + vy * vy;
			double b = 2.0 * (x2 * vx + y2 * vy);
			double c = x2 * x2 + y2 * y2 - radius * radius;
			double d = b * b - 4.0 * a * c;

			if (d < 0.0) {
				return double.PositiveInfinity;
			}

			double t = (-b - Math.Sqrt(d)) / (2.0 * a);
			return t <= 0.0 ? double.PositiveInfinity : t;
		}

		public double GetExitTime(Vector speed, Vector topLeft, Vector bottomRight) {
			// TODO: Use Math to calculate this

			Vector pos = this;
			int ticks = 0;
			while (pos.IsInRectangle(topLeft, bottomRight)) {
				pos += speed;
				ticks++;
			}

			return ticks;
		}

		public double GetAngleTo(Vector p2) {
			(double x, double y) = p2;
			return Math.Atan2(y - Y, x - X);
		}

		public Vector Rotate(Vector origin, double angleDegrees) {
			double radians = (Math.PI / 180) * angleDegrees;
			double cos = Math.Cos(radians);
			double sin = Math.Sin(radians);
			double newX = (cos * (X - origin.X)) + (sin * (Y - origin.Y)) + origin.X;
			double newY = (cos * (Y - origin.Y)) - (sin * (X - origin.X)) + origin.Y;
			return new Vector((int)newX, (int)newY);
		}

		public Vector Normalize() {
			double length = Length;
			if (length == 0) {
				return Zero;
			}

			return this / length;
		}

		public Vector Truncate() {
			return new Vector((int)X, (int)Y);
		}

		public Vector Truncate(int maxLength) {
			double length = Length;
			if (length <= maxLength) {
				return this;
			}

			var ratio = maxLength / length;
			return this * ratio;
		}

		public double DotMultiply(Vector otherVector) {
			var n1 = Normalize();
			var n2 = otherVector.Normalize();
			return n1.X * n2.X + n1.Y * n2.Y;
		}

		public Vector Clockwise() {
			return new Vector(-1 * Y, X);
		}

		public Vector CounterClockwise() {
			return new Vector(Y, -1 * X);
		}

		public IReadOnlyCollection<Vector> GetPointsInRadius(int radius, int clampToNearest = 1) {
			HashSet<Vector> points = new HashSet<Vector>();
			for (double x = X - radius; x <= X; x += clampToNearest) {
				for (double y = Y - radius; y <= Y; y += clampToNearest) {
					Vector point = new Vector(x, y);
					if (Distance2To(point) <= radius * radius) {
						double xSym = X - (x - X);
						double ySym = Y - (y - Y);

						points.Add(point);
						points.Add(new Vector(x, ySym));
						points.Add(new Vector(xSym, y));
						points.Add(new Vector(xSym, ySym));
					}
				}
			}

			return points;
		}

		public bool IsInRectangle(Vector topLeft, Vector bottomRight) {
			return X >= topLeft.X && X <= bottomRight.X && Y >= topLeft.Y && Y <= bottomRight.Y;
		}

		public void Deconstruct(out double x, out double y) {
			x = X;
			y = Y;
		}

		public override string ToString() {
			return $"{X.ToString(CultureInfo.InvariantCulture)} {Y.ToString(CultureInfo.InvariantCulture)}";
		}
	}
}
