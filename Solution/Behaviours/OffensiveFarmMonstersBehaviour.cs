using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class OffensiveFarmMonstersBehaviour : Sequence {
		public OffensiveFarmMonstersBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Attacker, 1)),
			new GetOffensiveFarmingLocation(),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetLocation()),
					new WaitForOtherHeroesToProcess()
				}),
				new Sequence(new NodeList {
					new SetHeroRole(HeroRole.Attacker),
					new Selector(new NodeList {
						new Sequence(new NodeList {
							new Inverter(new AmIWithinRangeOfMyTargetLocation(5000)),
							new MoveToMyTargetLocation()
						}),
						new Sequence(new NodeList {
							new GetMonstersNearMe(3000),
							new FilterEntitiesWithinRangeOfMyTargetLocation(3000),
							new FilterEntitesAlreadyBeingTargeted(),
							new TargetEntityClosestToMe(),
							new InterceptMyTarget()
						}),
						new MoveToMyTargetLocation()
					})
				})
			})
		}) {
		}
	}
}
