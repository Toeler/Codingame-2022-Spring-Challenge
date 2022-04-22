using System;
using System.Diagnostics;

namespace Lib {
	public class Timer {
		public readonly TimeSpan Duration;
		private readonly Stopwatch _sw;

		public Timer(TimeSpan duration)
		{
			_sw = Stopwatch.StartNew();
			Duration = duration;
		}

		public Timer(double durationMs)
			: this(TimeSpan.FromMilliseconds(durationMs))
		{
		}

		public TimeSpan TimeElapsed => _sw.Elapsed;

		public TimeSpan TimeAvailable => IsFinished() ? TimeSpan.Zero : Duration - TimeElapsed;

		public static Timer operator /(Timer timer, double v)
		{
			return new Timer(timer.TimeAvailable.TotalMilliseconds / v);
		}

		public static Timer operator *(Timer timer, double v)
		{
			return new Timer(timer.TimeAvailable.TotalMilliseconds * v);
		}

		public static Timer operator *(double v, Timer timer)
		{
			return new Timer(timer.TimeAvailable.TotalMilliseconds * v);
		}

		public bool IsFinished()
		{
			return _sw.Elapsed >= Duration;
		}

		public override string ToString()
		{
			return $"Elapsed {TimeElapsed.TotalMilliseconds:0} ms. Available {TimeAvailable.TotalMilliseconds:0} ms";
		}
	}
}
