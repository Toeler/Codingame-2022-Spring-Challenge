using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.BehaviourTree.Nodes {
	public class Sequence<TEntity, TState, TCommands, TGlobalCache, TEntityCache> : AbstractNodeWithChildren<IEnumerable<AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache>>, TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		public Sequence(IEnumerable<AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache>> children) : base(children) {
		}

		public override bool Execute(TEntity entity, TState state, TCommands chosenCommands, TGlobalCache globalCache, TEntityCache entityCache) {
#if DEBUG
			Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}All Of");
			Indents++;
#endif
			bool result = Children.Select(node => {
				bool result = node.Execute(entity, state, chosenCommands, globalCache, entityCache);
#if DEBUG
				Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}{node.GetType().Name}: {result}");
#endif
				return result;
			}).All(result => result);
#if DEBUG
			Indents--;
#endif
			return result;
		}
	}
}
