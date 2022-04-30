namespace Lib {
	public interface IStateReader<TInitialState, TState> {
		public TInitialState ReadInitialState();
		public TState ReadTurn(TInitialState initialState, TState previousState);
	}
}
