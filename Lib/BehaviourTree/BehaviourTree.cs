using System.Collections;
using Lib.BehaviourTree.Nodes;
using System.Collections.Generic;

namespace Lib.BehaviourTree {
	public abstract class BehaviourTree<TEntity, TState, TCommands, TCache> where TCache : IDictionary {
		private AbstractNode<TEntity, TState, TCommands, TCache> RootNode { get; }
		protected abstract TCache LocalCache { get; }

		protected BehaviourTree(AbstractNode<TEntity, TState, TCommands, TCache> rootNode) {
			RootNode = rootNode;
		}

		public void Execute(TEntity entity, TState state, TCommands chosenCommands) {
			LocalCache.Clear();
			RootNode.Execute(entity, state, chosenCommands, LocalCache);
		}
	}
}
