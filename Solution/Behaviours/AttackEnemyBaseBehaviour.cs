using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class AttackEnemyBaseBehaviour: Sequence {
		public AttackEnemyBaseBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Attacker, 1)),
			new DoWeHaveEnoughManaForASpell(reserveMana: 50),
			new GetMonstersWithinRangeOfEnemyBase(InitialState.FieldRadius + InitialState.WindSpellAttackRange),
			new FilterEntitiesWithinRangeOfMe(InitialState.WindSpellRange),
			new DoIHaveEnoughEntitiesInRange(1),
			new FilterEntitesAlreadyBeingTargeted(),
			new TargetEntityClosestToEnemyBase(),
			new SetHeroRole(HeroRole.Attacker),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetEntity()),
					new WaitForOtherHeroesToProcess()
				}),
				new PushMyTargetTowardsEnemyBase()
			})
		}) {
		}
	}
}
