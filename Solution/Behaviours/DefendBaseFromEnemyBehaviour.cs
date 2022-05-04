using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class DefendBaseFromEnemyBehaviour : Sequence {
		public DefendBaseFromEnemyBehaviour() : base(new NodeList {
			new Inverter(new DoWeHaveEnoughHeroesInRole(HeroRole.Defender, 1)),
			new GetEnemyHeroesNearMyBase(6000),
			new TargetEntityClosestToMe(),
			new SetHeroRole(HeroRole.Defender),
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
