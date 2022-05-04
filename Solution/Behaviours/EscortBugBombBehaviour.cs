using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class EscortBugBombBehaviour: Sequence {
		public EscortBugBombBehaviour() : base(new NodeList {
			new DoWeHaveEnoughManaForASpell(reserveMana: 30),
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Escorter, 2)),
			new GetMonstersThreateningEnemyBase(),
			new AreThereAtLeastXEntities(1),
			new TargetFirstEntityClosestToEnemyBase(),
			new AmIWithinRangeOfMyTargetEntity(6000),
			// new GetLocationsWithMostEntitiesInRadius(InitialState.ControlSpellRange, 2), // Too slow
			new GetAttackPosition(),
			// new TargetLocationClosestToMyTarget(),
			new SetHeroRole(HeroRole.Escorter),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetLocation()),
					new WaitForOtherHeroesToProcess()
				}),
				new MoveToMyTargetLocation()
			})
		}) {
		}
	}
}
