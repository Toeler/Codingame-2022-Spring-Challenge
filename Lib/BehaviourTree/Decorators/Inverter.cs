using System;
using Lib.BehaviourTree.Nodes;

namespace Lib.BehaviourTree.Decorators {
	public class Inverter<TEntity, TState, TCommands, TCache> : DecoratorNode<TEntity, TState, TCommands, TCache> {
		public Inverter(AbstractNode<TEntity, TState, TCommands, TCache> children) : base(children) {
		}

		public override bool Execute(TEntity entity, TState state, TCommands chosenCommands, TCache cache) {
#if DEBUG
			Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}One Of");
			Indents++;
#endif
			bool result = Children.Execute(entity, state, chosenCommands, cache);

#if DEBUG
			Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}{Children.GetType().Name}: {result}");
			Indents--;
#endif
			return !result;
		}
	}
}
