using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterLocationsAlreadyBeingTargeted : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (entityCache.TryGetValue(CacheKey.TargetLocations, out IEnumerable<Vector> locations)) {
				HashSet<Vector> locationsBeingTargeted = new HashSet<Vector>(chosenCommands.Values.Where(command => command is MoveCommand).Select(command => command.TargetPosition));

				entityCache[CacheKey.TargetLocations] = locations.Except(locationsBeingTargeted);
			}
			return true;
		}
	}
}
