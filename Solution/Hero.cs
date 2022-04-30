using System.Linq;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class Hero : AbstractEntity {
		public bool IsMine { get; }
		public override int MaxSpeed => InitialState.HeroMoveSpeed;

		public Hero(int id, Vector position, int shieldLife, bool isControlled, bool isMine, State previousState) : base(id, position, shieldLife, isControlled) {
			IsMine = isMine;
			Hero me = previousState?.AllHeroes.FirstOrDefault(hero => hero.Id == id);
			Vector = me != null ? Position - me.Position : Vector.Zero;
		}
	}
}
