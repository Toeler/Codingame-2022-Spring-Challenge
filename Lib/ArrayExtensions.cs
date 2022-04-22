using System.Linq;

namespace Lib {
	public static class ArrayExtensions {
		public static void Deconstruct<T>(this T[] list, out T first, out T second) {
			first = list.Length > 0 ? list[0] : default(T);
			second = list.Length > 1 ? list[1] : default(T);
		}

		public static void Deconstruct<T>(this T[] list, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T seventh, out T eighth, out T ninth, out T tenth, out T eleventh) {
			first = list.Length > 0 ? list[0] : default(T);
			second = list.Length > 1 ? list[1] : default(T);
			third = list.Length > 2 ? list[2] : default(T);
			fourth = list.Length > 3 ? list[3] : default(T);
			fifth = list.Length > 4 ? list[4] : default(T);
			sixth = list.Length > 5 ? list[5] : default(T);
			seventh = list.Length > 6 ? list[6] : default(T);
			eighth = list.Length > 7 ? list[7] : default(T);
			ninth = list.Length > 8 ? list[8] : default(T);
			tenth = list.Length > 9 ? list[9] : default(T);
			eleventh = list.Length > 10 ? list[10] : default(T);
		}

		public static void Deconstruct<T>(this T[] list, out T first, out T second, out T[] rest) {
			first = list.Length > 0 ? list[0] : default(T);
			second = list.Length > 1 ? list[1] : default(T);
			rest = list.Skip(2).ToArray();
		}
	}
}
