using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Processors {
	public class FilterEntitesAlreadyBeingTargeted: Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			if (cache.TryGetValue(CacheKey.TargetEntities, out IEnumerable<AbstractEntity> entities)) {
				HashSet<AbstractEntity> entitiesBeingTargeted = new HashSet<AbstractEntity>();

				foreach (var (_, command) in chosenCommands) {
					if (command.IsOfType<InterceptCommand, FarmMonstersCommand, UseControlSpellCommand, UseShieldSpellCommand>()) {
						entitiesBeingTargeted.Add(command.TargetEntity);
					}

					if (command is UseWindSpellCommand) {
						foreach (var enemy in state.EnemyHeroes.Concat<AbstractEntity>(state.Monsters)) {
							if (command.TargetPosition.DistanceTo(enemy.Position) <= InitialState.WindSpellRange) {
								entitiesBeingTargeted.Add(enemy);
							}
						}
					}
				}

				cache[CacheKey.TargetEntities] = entities.Except(entitiesBeingTargeted);
			}
			return true;
		}
	}
}
