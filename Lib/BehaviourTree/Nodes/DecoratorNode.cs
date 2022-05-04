namespace Lib.BehaviourTree.Nodes {
	public abstract class DecoratorNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> : AbstractNodeWithChildren<AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache>, TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		protected DecoratorNode(AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> children) : base(children) {
		}
	}
}
