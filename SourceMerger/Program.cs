using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SourceMerger {
	internal static class Program {
		private static readonly string[] IgnoredPatterns = {
			@"tests?\.cs",
			@"_tests?\.cs",
			@"_should.cs",
			@"_should.solution.cs",
			@"bin\\",
			@"obj\\",
			"Builder.cs",
			@".*/tests.*"
		};

		private const string UsingPattern = @"using [A-Za-z0-9. =]+;\r?\n";

		private const string OutputFile = @"..\dist\Program.cs";

		public static void Main(string[] args) {
			string[] dirs = args.Length > 0 ? args : new[] {@".", @"../Lib"};

			var sources =
				dirs.SelectMany(
					dir => Directory
						.EnumerateFiles(dir, "*.cs", SearchOption.AllDirectories)
						.Where(fn => !IgnoredPatterns.Any(p => Regex.IsMatch(fn, p, RegexOptions.IgnoreCase)))
						.Select(fn => fn.ToLower())
						.OrderBy(fn => fn)
						.Select(fn => new {name = fn.ToLower(), src = File.ReadAllText(fn)})
				).ToList();
			IList<string> exceptions = sources
				.Where(file => file.name.EndsWith(".solution.cs"))
				.Select(file => file.name.Replace(".solution.cs", ".cs"))
				.ToList();

			HashSet<string> usings = new HashSet<string>();
			StringBuilder sb = new StringBuilder();
			foreach (var file in sources) {
				if (exceptions.Contains(file.name)) {
					Console.WriteLine($"Skip {file.name}");
					continue;
				}

				Console.WriteLine($"Use {file.name}");
				string source = file.src;
				IList<string> usingLines = Regex.Matches(source, UsingPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase)
					.Select(m => m.Value)
					.ToList();

				foreach (string usingLine in usingLines) {
					if (!usingLine.StartsWith("using System") && !usingLine.StartsWith("using Lib")) {
						ConsoleColor oldColor = Console.ForegroundColor;
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(usingLine + " in " + file.name);
						Console.WriteLine("You can't use third party libs in your solution! Press any key and fix it!");
						Console.ForegroundColor = oldColor;
						Console.ReadLine();
						Environment.Exit(255);
					}

					usings.Add(usingLine);
				}

				string sourceWithNoUsings =
					Regex.Replace(source, UsingPattern, "", RegexOptions.Multiline | RegexOptions.IgnoreCase)
						.Trim();
				sb.AppendLine(sourceWithNoUsings);
				sb.AppendLine();
			}

			sb.Insert(0, string.Join("", usings) + "\r\n");
			string result = sb.ToString();
			Console.WriteLine($"Length: {result.Length}");
			if (result.Length > 90000) {
				result = Compress(result);
			}

			Console.WriteLine();
			FileInfo output = new FileInfo(OutputFile);
			output.Directory?.Create(); // If the directory already exists, this method does nothing.
			File.WriteAllText(output.FullName, result);
			Console.WriteLine($"Result saved to {output.FullName}");
		}


		private static string Compress(string result) {
			result = result.Replace("\r\n", "\n");
			result = Regex.Replace(result, @"\n[ \t]+", "\n");
			Console.WriteLine($"Compressed to: {result.Length}");
			return result;
		}
	}
}
