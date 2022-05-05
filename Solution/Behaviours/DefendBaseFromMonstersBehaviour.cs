using System.Collections.Generic;
using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Commands;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;
using Lib.BehaviourTree.Nodes;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class DefendBaseFromMonstersBehaviour : Sequence {
		public DefendBaseFromMonstersBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Defender, 1)),
			new GetMonstersWithinRangeOfMyBase(8500),
			new FilterEntitesAlreadyBeingTargeted(),
			new TargetFirstEntityClosestToMyBase(),
			new SetHeroRole(HeroRole.Defender),
			new Selector(new NodeList {
				new DefendAgainstMonsterSubRoutine(),
				new RepeaterUntilSuccess(3,
					new Sequence(new NodeList {
						new TargetNextEntityClosestToMyBase(),
						new DefendAgainstMonsterSubRoutine()
					})
				)
			})
		}) { }

		private class DefendAgainstMonsterSubRoutine : Sequence {
			public DefendAgainstMonsterSubRoutine() : base(new NodeList {
				new Selector(new NodeList {
					new Sequence(new NodeList {
						new Inverter(new AmIClosestToMyTargetEntity()),
						new WaitForOtherHeroesToProcess()
					}),
					new Sequence(new NodeList {
						new DoWeHaveEnoughManaForASpell(0),
						new Inverter(new IsMyTargetShielded()),
						new AmIWithinRangeOfMyTargetEntity(InitialState.WindSpellRange),
						new Selector(new NodeList {
							new Sequence(new NodeList {
								new IsThereEnemyHeroNearMyTarget(InitialState.WindSpellRange + InitialState.HeroMoveSpeed),
								new IsMyTargetWithinRangeOfMyBase(InitialState.WindSpellAttackRange + InitialState.MonsterMoveSpeed * 2)
							}),
							new Inverter(new CanIKillMyTargetBeforeItReachesMyBase())
						}),
						new PushMyTargetAwayFromMyBase() // TODO: Maybe sometimes tangentially away from enemy hero
					}),
					new Sequence(new NodeList {
						new DoWeHaveEnoughManaForASpell(0),
						new Inverter(new IsMyTargetShielded()),
						new Inverter(new IsMyTargetControlled()),
						new AmIWithinRangeOfMyTargetEntity(InitialState.ControlSpellRange),
						new Inverter(new CanIKillMyTargetBeforeItReachesMyBase()),
						new Selector(new NodeList {
							new Sequence(new NodeList {
								new WillMyTargetEnterMyBaseNextTurn(), // TODO: Maybe sometimes more than "next turn"?
								new RedirectMyTargetAwayFromMyBase()
							}),
							new Sequence(new NodeList {
								new IsMyTargetWithinRangeOfMyBase(InitialState.WindSpellAttackRange),
								new IsMyTargetCloserToMyBaseThanMe(),
								new Inverter(new IsMyTargetWithinRangeOfMyBase(InitialState.MonsterMoveSpeed + InitialState.BaseRadius)),
								new RedirectMyTargetTowardsMe()
							})
						})
					}),
					new Sequence(new NodeList {
						new Inverter(new Sequence(new NodeList {
							new Inverter(new CanIGetInRangeOfMyTargetBeforeItReachesMyBase(InitialState.WindSpellRange)),
							new AddMyTargetToIgnoreList() // Pick the next target
						})),
						new Selector(new NodeList {
							new Sequence(new NodeList {
								new AmIWithinRangeOfMyTargetEntity(InitialState.HeroAttackRadius),
								new IsThereAnotherMonsterThatINeedToPushBack(),
								new RedirectMyTargetAwayFromMyBase()
							}),
							new InterceptMyTarget()
						})
					})
				})
			}) { }
		}

		private class TemporaryTargetSwitch : DecoratorNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
			public TemporaryTargetSwitch(AbstractNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> children) : base(children) { }

			public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
				entityCache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> targetEntities);
				entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity);

				bool result = Children.Execute(entity, state, chosenCommands, globalCache, entityCache);

				if (targetEntities != null) {
					entityCache[CacheKey.TargetEntities] = targetEntities;
					entityCache[CacheKey.TargetEntity] = targetEntity;
				}

				return result;
			}
		}

		private class IsThereAnotherMonsterThatINeedToPushBack : Sequence {
			public IsThereAnotherMonsterThatINeedToPushBack() : base(new NodeList {
				new DoWeHaveEnoughManaForASpell(0),
				new TemporaryTargetSwitch(new Sequence(new NodeList {
					new GetMonstersNearMe(InitialState.WindSpellRange),
					new FilterEntitiesThatAreNotMyTarget(),
					new FilterEntitesAlreadyBeingTargeted(),
					new FilterEntitiesThatICantGetToInTime(),
					new TargetFirstEntityClosestToMyBase(),
					new Inverter(new IsMyTargetShielded()),
					new Selector(new NodeList {
						new Sequence(new NodeList {
							new IsThereEnemyHeroNearMyTarget(InitialState.WindSpellRange + InitialState.HeroMoveSpeed),
							new IsMyTargetWithinRangeOfMyBase(InitialState.WindSpellAttackRange + InitialState.MonsterMoveSpeed)
						}),
						new Inverter(new CanIKillMyTargetBeforeItReachesMyBase())
					})
				}))
			}) { }
		}
	}
}
