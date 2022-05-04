using Codingame_2022_Spring_Challenge.Commands;
using Lib;
using System.Collections.Generic;

namespace Codingame_2022_Spring_Challenge.Actions {
	public class RedirectMyTargetAwayFromMyBase : Leaf {
		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache globalCache, BehaviourCache entityCache) {
			if (!entityCache.TryGetValue(CacheKey.Role, out HeroRole role)) {
				role = HeroRole.None;
			}
			if (!entityCache.TryGetValue(CacheKey.TargetEntity, out AbstractEntity targetEntity)) {
				return false;
			}

			if (!(targetEntity is Monster target)) {
				return false;
			}

			Vector expectedPosition = target.Position + target.Vector;
			Vector newVectorAwayFromMyBase = (expectedPosition - state.MyBase).Normalize() * InitialState.MonsterMoveSpeed;

			Vector redirectVelocity;
			if (state.MyBase == Vector.Zero) {
				redirectVelocity = new Vector(1, 0).DotMultiply(newVectorAwayFromMyBase) >=
				                    new Vector(1, 0).DotMultiply(new Vector(1, 1))
					? newVectorAwayFromMyBase.Clockwise()
					: newVectorAwayFromMyBase.CounterClockwise();
			} else {
				redirectVelocity =
					new Vector(-1, 0).DotMultiply(newVectorAwayFromMyBase) >=
					new Vector(-1, 0).DotMultiply(new Vector(-1, -1))
						? newVectorAwayFromMyBase.Clockwise()
						: newVectorAwayFromMyBase.CounterClockwise();
			}

			Vector redirectToLocation = (expectedPosition + redirectVelocity).Truncate();

			chosenCommands.Add(entity, new UseControlSpellCommand(target, redirectToLocation, role));
			return true;
		}
	}
}
