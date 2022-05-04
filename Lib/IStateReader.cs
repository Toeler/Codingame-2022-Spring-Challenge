namespace Lib {
	public interface IStateReader<TInitialState, TState> {
		public TInitialState ReadInitialState();
		public TState ReadTurn(int turnNumber, TInitialState initialState, TState previousState);
	}
}
