using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class FarmMonstersSubRoutine<TGetTargetLocation> : Sequence where TGetTargetLocation : Leaf, new() {
		public FarmMonstersSubRoutine(HeroRole role, int farmingRange = 3000) : base(new NodeList {
			new TGetTargetLocation(),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetLocation()),
					new WaitForOtherHeroesToProcess()
				}),
				new Sequence(new NodeList {
					new SetHeroRole(role),
					new Selector(new NodeList {
						new Sequence(new NodeList {
							new Selector(new NodeList {
								new Sequence(new NodeList {
									new GetMonstersWithinRangeOfMyTargetLocation(3000),
									new FilterOutEntitiesThatArentTargetingMyBase(),
									new TargetFirstEntityClosestToMyBase()
								}),
								new Sequence(new NodeList {
									new GetMonstersWithinRangeOfMyTargetLocation(3000),
									new TargetFirstEntityClosestToMyBase()
								})
							}),
							new InterceptMyTarget()
						}),
						new Sequence(new NodeList {
							new TGetTargetLocation(),
							new MoveToMyTargetLocation()
						})
					})
				})
			})
		}) { }
	}

	public class DefensiveFarmMonstersBehaviour : Sequence {
		public DefensiveFarmMonstersBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Defender, 1)),
			new FarmMonstersSubRoutine<GetDefensiveFarmingLocation>(HeroRole.Defender)
		}) { }
	}

	public class OffensiveFarmMonstersBehaviour : Sequence {
		public OffensiveFarmMonstersBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRoles(new[] {HeroRole.Attacker, HeroRole.Midfielder}, 2)),
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Attacker, 1)),
			new FarmMonstersSubRoutine<GetOffensiveFarmingLocation>(HeroRole.Attacker)
		}) { }
	}

	public class MidfielderFarmMonstersBehaviour : Sequence {
		public MidfielderFarmMonstersBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRoles(new[] {HeroRole.Attacker, HeroRole.Midfielder}, 2)),
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Midfielder, 1)),
			new FarmMonstersSubRoutine<GetMidfieldFarmingLocation>(HeroRole.Midfielder, 2000)
		}) { }
	}
}
