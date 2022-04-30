using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class CreateBugBombBehaviour : Sequence {
		public CreateBugBombBehaviour() : base(new NodeList {
			new DoWeHaveEnoughMana(requiredMana: 50, reserveMana: 50),
			new GetMonstersWithinRangeOfEnemyBase(10000),
			new FilterEntitiesWithShield(3),
			new DoIHaveEnoughEntitiesInRange(3),
			new FilterMonstersWithThreatFor(Target.EnemyBase),
			new FilterEntitiesWithinRangeOfMe(InitialState.ControlSpellRange),
			new FilterEntitesAlreadyBeingTargeted(),
			new Selector(new NodeList {
				new TargetMonsterAboutToLeaveTheMap(2),
				new TargetEntityFarthestFromEnemyBase(),
			}),
			new SetHeroRole(HeroRole.Exterminator),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetEntity()),
					new WaitForOtherHeroesToProcess()
				}),
				new RedirectMyTargetTowardsEnemyBase()
			})
		}) {
		}
	}
}
