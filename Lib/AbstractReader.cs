using System.Linq;

namespace Lib {
	public abstract class AbstractReader {
		public int ReadInt() {
			return int.Parse(ReadLine());
		}

		public int[] ReadInts() {
			return ReadLine().Split(' ').Select(int.Parse).ToArray();
		}

		public string[] ReadStrings() {
			return ReadLine().Split(' ').ToArray();
		}

		public Vector ReadVector() {
			(int x, int y, int[] _) = ReadInts();
			return new Vector(x, y);
		}

		protected abstract string ReadLine();

		public virtual void Flush() {
			// Do nothing
		}
	}
}
