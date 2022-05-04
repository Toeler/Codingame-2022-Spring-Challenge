using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class AttackEnemyBaseBehaviour: Sequence {
		public AttackEnemyBaseBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Attacker, 2)),
			new DoWeHaveEnoughManaForASpell(reserveMana: 0),
			new GetMonstersWithinRangeOfEnemyBase(InitialState.FieldRadius + InitialState.WindSpellRange),
			new FilterEntitiesWithinRangeOfMe(InitialState.WindSpellRange),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new DoIHaveEnoughEntitiesInRange(1),
					new TargetFirstEntityClosestToEnemyBase(),
					new SetHeroRole(HeroRole.Attacker),
					new Selector(new NodeList {
						new Sequence(new NodeList {
							new Inverter(new AmIClosestToMyTargetEntity()),
							new WaitForOtherHeroesToProcess()
						}),
						new PushMyTargetTowardsEnemyBase()
					})
				}),
				new Sequence(new NodeList {
					new AmIWithinRangeOfEnemyBase(InitialState.FieldRadius + InitialState.WindSpellRange),
					new GetMonstersWithinRangeOfEnemyBase(InitialState.FieldRadius),
					new DoIHaveEnoughEntitiesInRange(1),
					new TargetEntityClosestToMe(),
					new SetHeroRole(HeroRole.Attacker),
					new InterceptMyTarget()
				})
			})
		}) {
		}
	}
}
