using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class DefendBaseFromMonstersBehaviour : Sequence {
		private class DefendAgainstMonsterSubRoutine : Selector {
			public DefendAgainstMonsterSubRoutine(): base(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetEntity()),
					new WaitForOtherHeroesToProcess()
				}),
				new Sequence(new NodeList {
					new DoWeHaveEnoughManaForASpell(reserveMana: 0),
					new Inverter(new IsMyTargetShielded()),
					new Selector(new NodeList {
						new Sequence(new NodeList {
							new IsThereEnemyHeroNearMyTarget(InitialState.WindSpellRange + InitialState.HeroMoveSpeed),
							new IsMyTargetWithinRangeOfMyBase(range: InitialState.WindSpellAttackRange + InitialState.MonsterMoveSpeed*2)
						}),
						new Inverter(new CanIKillMyTargetBeforeItReachesMyBase())
					}),
					new AmIWithinRangeOfMyTargetEntity(range: InitialState.WindSpellRange),
					new PushMyTargetAwayFromMyBase()
				}),
				new Sequence(new NodeList {
					new Inverter(new CanIKillMyTargetBeforeItReachesMyBase()),
					new DoWeHaveEnoughManaForASpell(reserveMana: 0),
					new Inverter(new IsMyTargetShielded()),
					new Inverter(new IsMyTargetControlled()),
					new Selector(new NodeList {
						new WillMyTargetEnterMyBaseNextTurn(),
						new IsMyTargetWithinRangeOfMyBase(range: 2200),
					}),
					new IsMyTargetWithinRangeOfMyBase(range: InitialState.MonsterBaseDetectionRange),
					new AmIWithinRangeOfMyTargetEntity(range: InitialState.ControlSpellRange),
					new Selector(new NodeList {
						new Sequence(new NodeList {
							new WillMyTargetEnterMyBaseNextTurn(),
							new RedirectMyTargetAwayFromMyBase()
						}),
						new Sequence(new NodeList {
							new IsMyTargetWithinRangeOfMyBase(range: 2200),
							new RedirectMyTargetTowardsMe()
						})
					}),
				}),
				new Sequence(new NodeList {
					new Inverter(new Sequence(new NodeList {
						new Inverter(new CanIGetInRangeOfMyTargetBeforeItReachesMyBase(InitialState.WindSpellRange)), // If no, pick the next target
						new AddMyTargetToIgnoreList(),
					})),
					new InterceptMyTarget()
				})
			}) {}
		}

		public DefendBaseFromMonstersBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Defender, InitialState.MaximumDefenders)),
			new GetMonstersThreateningMyBase(),
			new FilterEntitesAlreadyBeingTargeted(),
			new FilterEntitiesWithinRangeOfMyBase(7000),
			new FilterEntitiesThatICantGetToInTime(),
			new TargetFirstEntityClosestToMyBase(),
			new SetHeroRole(HeroRole.Defender),
			new Inverter(new Sequence(new NodeList {
				new Inverter(new DefendAgainstMonsterSubRoutine()),
				new Inverter(new RepeaterUntilSuccess(3,
					new Sequence(new NodeList {
						new TargetNextEntityClosestToMyBase(),
						new DefendAgainstMonsterSubRoutine()
					})
				))
			}))
		}) {
		}
	}
}

// TODO: Need to check if enemy hero is able to double wind a monster and move that monster away from the enemy hero
