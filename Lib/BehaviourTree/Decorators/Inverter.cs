using System;
using Lib.BehaviourTree.Nodes;

namespace Lib.BehaviourTree.Decorators {
	public class Inverter<TEntity, TState, TCommands, TGlobalCache, TEntityCache> : DecoratorNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		public Inverter(AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> children) : base(children) {
		}

		public override bool Execute(TEntity entity, TState state, TCommands chosenCommands, TGlobalCache globalCache, TEntityCache entityCache) {
#if DEBUG
			Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}One Of");
			Indents++;
#endif
			bool result = Children.Execute(entity, state, chosenCommands, globalCache, entityCache);

#if DEBUG
			Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}{Children.GetType().Name}: {result}");
			Indents--;
#endif
			return !result;
		}
	}
}
