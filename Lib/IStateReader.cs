namespace Lib {
	public interface IStateReader<TInitialState, out TState> {
		public TInitialState ReadInitialState();
		public TState ReadTurn(TInitialState initialState);
	}
}
