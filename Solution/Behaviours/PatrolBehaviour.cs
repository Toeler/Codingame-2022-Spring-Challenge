using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class PatrolBehaviour : Sequence {
		public PatrolBehaviour() : base(new NodeList {
			new GetPatrolLocations(),
			new FilterLocationsAlreadyBeingTargeted(),
			new FilterLocationsWithMonsters(),
			new FilterLocationsThatIJustVisited(),
			new TargetLocationClosestToMe(),
			new SetHeroRole(HeroRole.Explorer),
			new Selector(new NodeList {
				new Sequence(new NodeList {
					new Inverter(new AmIClosestToMyTargetLocation()),
					new WaitForOtherHeroesToProcess()
				}),
				new MoveToMyTargetLocation()
			})
		}) {
		}
	}
}
