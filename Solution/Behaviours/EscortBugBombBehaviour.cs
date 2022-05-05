using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class EscortBugBombBehaviour : Sequence {
		public EscortBugBombBehaviour() : base(new NodeList {
			new IsItAtLeastTurnNumber(70),
			new DoWeHaveEnoughManaForASpell(30),
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Attacker, 2)),
			new GetMonstersThreateningEnemyBase(),
			new AreThereAtLeastXEntities(1),
			new TargetFirstEntityClosestToEnemyBase(),
			new AmIWithinRangeOfMyTargetEntity(6000),
			new GetAttackPosition(),
			new SetHeroRole(HeroRole.Attacker),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetLocation()),
					new WaitForOtherHeroesToProcess()
				}),
				new MoveToMyTargetLocationAtSpeed(InitialState.MonsterMoveSpeed)
			})
		}) { }
	}
}
