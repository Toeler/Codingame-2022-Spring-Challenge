using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib {
	public class DebugReader : AbstractReader {
		private int _currentLine;
		private readonly string[] _textLines;

		public DebugReader(string input = null) {
			_textLines = input?.Split('|');
		}

		protected override string ReadLine() {
			return _textLines[_currentLine++];
		}
	}
}
