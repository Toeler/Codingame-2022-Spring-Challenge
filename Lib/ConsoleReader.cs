using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib {
	public class ConsoleReader : AbstractReader {
		private readonly IList<string> _linesFromConsole = new List<string>();

		protected override string ReadLine() {
			_linesFromConsole.Add(Console.ReadLine());
			return _linesFromConsole.Last();
		}

		public override void Flush() {
			Console.Error.WriteLine(string.Join('|', _linesFromConsole));
			_linesFromConsole.Clear();
		}
	}
}
