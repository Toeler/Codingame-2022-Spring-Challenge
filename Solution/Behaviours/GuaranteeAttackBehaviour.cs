using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Conditions;
using Codingame_2022_Spring_Challenge.Processors;

namespace Codingame_2022_Spring_Challenge.Behaviours {
	public class GuaranteeAttackBehaviour : Sequence {
		public GuaranteeAttackBehaviour() : base(new NodeList {
			new DoWeHaveEnoughManaForASpell(reserveMana: 0),
			new GetMonstersThatWillReachEnemyBaseInTurns(InitialState.ShieldTime),
			new FilterEntitiesWithShield(),
			new FilterEntitesAlreadyBeingTargeted(),
			new TargetEntityClosestToEnemyBase(),
			new AmIWithinRangeOfMyTargetEntity(InitialState.ShieldSpellRange),
			new ShieldMyTarget()
		}) {
		}
	}
}
