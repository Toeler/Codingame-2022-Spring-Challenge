using System;
using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class StateReader : IStateReader<InitialState, State> {
		private readonly AbstractReader _reader;

		public StateReader(AbstractReader reader) {
			_reader = reader;
		}

		public InitialState ReadInitialState() {
			Vector basePos = _reader.ReadVector();
			int heroesPerPlayer = _reader.ReadInt();

			return new InitialState(basePos, heroesPerPlayer);
		}

		public State ReadTurn(InitialState initialState, State previousState) {
			(int myHealth, int myMana) = _reader.ReadInts();
			(int enemyHealth, int enemyMana) = _reader.ReadInts();

			int entityCount = _reader.ReadInt();
			AbstractEntity[] entities = Enumerable.Range(0, entityCount).Select(_ => ReadEntity(previousState)).ToArray();

			return new State(initialState, myHealth, myMana, enemyHealth, enemyMana, entities);
		}

		private AbstractEntity ReadEntity(State previousState) {
			(int id, int type, int x, int y, int shieldLife, int isControlled, int health, int vx, int vy, int nearBase, int threatFor) = _reader.ReadInts();

			if (type == 0) {
				return new Monster(id, new Vector(x, y), shieldLife, isControlled == 1, health, new Vector(vx, vy), nearBase == 1, (Target)threatFor);
			} else {
				return new Hero(id, new Vector(x, y), shieldLife, isControlled == 1, type == 1, previousState);
			}
		}
	}
}
