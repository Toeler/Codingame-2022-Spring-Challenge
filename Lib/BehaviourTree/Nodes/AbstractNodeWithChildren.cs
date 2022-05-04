namespace Lib.BehaviourTree.Nodes {
	public abstract class AbstractNodeWithChildren<TChildren, TEntity, TState, TCommands, TGlobalCache, TEntityCache> : AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		public TChildren Children { get; }

		public AbstractNodeWithChildren(TChildren children) {
			Children = children;
		}
	}
}
