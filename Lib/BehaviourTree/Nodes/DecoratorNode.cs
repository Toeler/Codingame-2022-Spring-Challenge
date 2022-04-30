namespace Lib.BehaviourTree.Nodes {
	public abstract class DecoratorNode<TEntity, TState, TCommands, TCacheKey> : AbstractNodeWithChildren<AbstractNode<TEntity, TState, TCommands, TCacheKey>, TEntity, TState, TCommands, TCacheKey> {
		protected DecoratorNode(AbstractNode<TEntity, TState, TCommands, TCacheKey> children) : base(children) {
		}
	}
}
