using System.Collections.Generic;

namespace Lib.BehaviourTree.Nodes {
	public abstract class CompositeNode<TEntity, TState, TCommands, TCacheKey> : AbstractNodeWithChildren<IEnumerable<AbstractNode<TEntity, TState, TCommands, TCacheKey>>, TEntity, TState, TCommands, TCacheKey> {
		protected CompositeNode(IEnumerable<AbstractNode<TEntity, TState, TCommands, TCacheKey>> children) : base(children) {
		}
	}
}
