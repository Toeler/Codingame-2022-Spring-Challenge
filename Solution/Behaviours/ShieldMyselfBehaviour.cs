using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class ShieldMyselfBehaviour : Sequence {
		public ShieldMyselfBehaviour() : base(new NodeList {
			new AmIWithinRangeOfMyBase(6000),
			new CanISeeEnemyHero(),
			new Inverter(new DoIHaveShield()),
			new DoWeHaveEnoughManaForASpell(reserveMana: 50),
			new GetMonstersThreateningMyBase(),
			new AreThereAtLeastXEntities(1),
			new TargetEntityClosestToMe(),
			new AmIWithinRangeOfMyTargetEntity(3000),
			new ShieldMyself()
		}) {
		}
	}
}
