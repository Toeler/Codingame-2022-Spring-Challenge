using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class DefensiveFarmMonstersBehaviour : Sequence {
		public DefensiveFarmMonstersBehaviour() : base(new NodeList {
			new GetWanderingMonsters(),
			new FilterEntitiesWithinRangeOfMyBase(InitialState.FarmingRange),
			new FilterEntitesAlreadyBeingTargeted(),
			new TargetEntityClosestToMe(),
			new SetHeroRole(HeroRole.Farmer),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetEntity()),
					new WaitForOtherHeroesToProcess()
				}),
				new InterceptMyTarget()
			})
		}) {
		}
	}
}
