using System;
using Lib.BehaviourTree.Nodes;

namespace Lib.BehaviourTree.Decorators {
	public class Repeater<TEntity, TState, TCommands, TGlobalCache, TEntityCache> : DecoratorNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		private readonly int _repetitions;

		public Repeater(int repetitions, AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> children) : base(children) {
			_repetitions = repetitions;
		}

		protected virtual bool ShouldStop(bool? result) {
			return false;
		}

		public override bool Execute(TEntity entity, TState state, TCommands chosenCommands, TGlobalCache globalCache, TEntityCache entityCache) {
#if DEBUG
			Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}One Of");
			Indents++;
#endif
			int iterations = 0;
			bool? result = null;
			while (iterations < _repetitions && !ShouldStop(result)) {
				result = Children.Execute(entity, state, chosenCommands, globalCache, entityCache);
				iterations++;
			}

#if DEBUG
			Console.Error.WriteLine($"{string.Empty.PadRight(Indents, ' ')}{Children.GetType().Name}: {result}");
			Indents--;
#endif
			return result == true;
		}
	}

	public class RepeaterUntilFail<TEntity, TState, TCommands, TGlobalCache, TEntityCache> : Repeater<TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		public RepeaterUntilFail(int repetitions, AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> children) : base(repetitions, children) { }

		protected override bool ShouldStop(bool? result) {
			return result == false;
		}
	}

	public class RepeaterUntilSuccess<TEntity, TState, TCommands, TGlobalCache, TEntityCache> : Repeater<TEntity, TState, TCommands, TGlobalCache, TEntityCache> {
		public RepeaterUntilSuccess(int repetitions, AbstractNode<TEntity, TState, TCommands, TGlobalCache, TEntityCache> children) : base(repetitions, children) { }

		protected override bool ShouldStop(bool? result) {
			return result == true;
		}
	}
}
