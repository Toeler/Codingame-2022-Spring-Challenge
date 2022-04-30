using System.Collections.Generic;

namespace Lib.BehaviourTree.Nodes {
	public abstract class AbstractNode<TEntity, TState, TCommands, TCache> {
#if true
		protected static int Indents = 0;
#endif
		public abstract bool Execute(TEntity entity, TState state, TCommands chosenCommands, TCache cache);
	}
}
