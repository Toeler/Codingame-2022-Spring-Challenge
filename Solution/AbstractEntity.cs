using Lib;

namespace Codingame_2022_Spring_Challenge {
	public abstract class AbstractEntity {
		public int Id { get; private set; }
		public Vector Position { get; private set; }
		public int ShieldLife { get; private set; }
		public bool IsControlled { get; private set; }

		public AbstractEntity(int id, Vector position, int shieldLife, bool isControlled) {
		    Id = id;
		    Position = position;
		    ShieldLife = shieldLife;
		    IsControlled = isControlled;
		}
	}
}
