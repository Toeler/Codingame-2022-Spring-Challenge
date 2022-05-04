using System.Collections.Generic;

namespace Lib.BehaviourTree.Nodes {
	public abstract class CompositeNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> : AbstractNodeWithChildren<IEnumerable<AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache>>, TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		protected CompositeNode(IEnumerable<AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache>> children) : base(children) {
		}
	}
}
