using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class DefendBaseFromMonstersBehaviour : Sequence {
		public DefendBaseFromMonstersBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Defender, InitialState.MaximumDefenders)),
			new GetMonstersThreateningMyBase(),
			new FilterEntitesAlreadyBeingTargeted(),
			new FilterShieldedMonstersThatWillReachMyBase(),
			new TargetEntityClosestToMyBase(),
			new SetHeroRole(HeroRole.Defender),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetEntity()),
					new WaitForOtherHeroesToProcess()
				}),
				new Sequence(new NodeList {
					new DoWeHaveEnoughManaForASpell(reserveMana: 0),
					new Inverter(new IsMyTargetShielded()),
					new IsMyTargetWithinRangeOfMyBase(range: InitialState.FieldRadius),
					new AmIWithinRangeOfMyTargetEntity(range: InitialState.WindSpellRange),
					new PushMyTargetAwayFromMyBase()
				}),
				new Sequence(new NodeList {
					new DoWeHaveEnoughManaForASpell(reserveMana: 0),
					new Inverter(new IsMyTargetShielded()),
					new Inverter(new IsMyTargetControlled()),
					new Inverter(new WillMyTargetEnterMyBaseNextTurn()),
					new IsMyTargetWithinRangeOfMyBase(range: InitialState.MonsterBaseDetectionRange),
					new AmIWithinRangeOfMyTargetEntity(range: InitialState.ControlSpellRange),
					new RedirectMyTargetAwayFromMyBase()
				}),
				new InterceptMyTarget()
			})
		}) {
		}
	}
}
