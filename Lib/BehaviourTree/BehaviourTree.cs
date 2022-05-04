using System.Collections;
using Lib.BehaviourTree.Nodes;
using System.Collections.Generic;

namespace Lib.BehaviourTree {
	public abstract class BehaviourTree<TEntity, TState, TCommands, TGlobalCache, TEntityCache> where TGlobalCache : IDictionary where TEntityCache : IDictionary {
		private AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> RootNode { get; }
		protected abstract TEntityCache LocalCache { get; }
		protected abstract TGlobalCache GlobalCache { get; }

		protected BehaviourTree(AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> rootNode) {
			RootNode = rootNode;
		}

		public void Execute(TEntity entity, TState state, TCommands chosenCommands) {
			LocalCache.Clear();
			RootNode.Execute(entity, state, chosenCommands, GlobalCache, LocalCache);
		}
	}
}
