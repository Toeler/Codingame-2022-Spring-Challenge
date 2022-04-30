namespace Lib.BehaviourTree.Nodes {
	public abstract class AbstractNodeWithChildren<TChildren, TEntity, TState, TCommands, TCacheKey> : AbstractNode<TEntity, TState, TCommands, TCacheKey> {
		public TChildren Children { get; }

		public AbstractNodeWithChildren(TChildren children) {
			Children = children;
		}
	}
}
