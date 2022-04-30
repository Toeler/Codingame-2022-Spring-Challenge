using Codingame_2022_Spring_Challenge.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class CanISeeEnemyHero : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			return state.EnemyHeroes.Select(enemyHero =>
				entity.Position.Distance2To(enemyHero.Position) <= Math.Pow(InitialState.HeroVisionRange, 2)
			).Any(canISee => canISee);
		}
	}
}
