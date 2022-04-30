using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class ShieldMyselfBehaviour : Sequence {
		public ShieldMyselfBehaviour() : base(new NodeList {
			new AmIWithinRangeOfEnemyBase(7000),
			new CanISeeEnemyHero(),
			new Inverter(new DoIHaveShield()),
			new DoWeHaveEnoughManaForASpell(reserveMana: 50),
			new ShieldMyself()
		}) {
		}
	}
}
