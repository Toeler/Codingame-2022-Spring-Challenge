#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lib {
	public abstract class Command
	{
		public abstract override string ToString();
	}

	public class Command<TSelf> : Command where TSelf : Command<TSelf> {
		private static readonly FieldInfo[] Fields = typeof(TSelf).GetFields(BindingFlags.Instance | BindingFlags.Public);

		private static readonly FieldInfo[] Args = typeof(TSelf)
			.GetConstructors().Single().GetParameters()
			.Select(p => Fields.Single(f => f.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)))
			.ToArray();

		public string? Message;

		public override string ToString() {
			string name = GetType().Name.ToUpper();
			IEnumerable<object?> args = Args.Select(p => p.GetValue(this)).Prepend(name);
			if (!string.IsNullOrWhiteSpace(Message)) {
				args = args.Append(Message);
			}

			return string.Join(' ', args);
		}
	}
}
