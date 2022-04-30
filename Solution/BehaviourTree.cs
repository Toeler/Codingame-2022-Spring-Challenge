using System.Collections.Generic;
using Codingame_2022_Spring_Challenge.Actions;
using Codingame_2022_Spring_Challenge.Behaviours;
using Codingame_2022_Spring_Challenge.Commands;
using Codingame_2022_Spring_Challenge.Conditions;
using Lib.BehaviourTree;
using Lib.BehaviourTree.Decorators;
using Lib.BehaviourTree.Nodes;

namespace Codingame_2022_Spring_Challenge {
	public enum CacheKey {
		Role,
		TargetEntities,
		TargetEntity,
		TargetLocations,
		TargetLocation
	}

	public class BehaviourCache : Dictionary<CacheKey, object> {
		public bool TryGetValue<T>(CacheKey key, out T value) {
			bool result = TryGetValue(key, out var dynamicValue);
			value = dynamicValue is T typedValue ? typedValue : default;
			return result;
		}
	}

	public class NodeList : List<AbstractNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache>> {}

	public class Inverter : Inverter<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache> {
		public Inverter(AbstractNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache> children) : base(children) {
		}
	}

	public abstract class Leaf : Leaf<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache> {
		protected HeroRole GetRoleOrDefault(BehaviourCache cache) {
			if (!cache.TryGetValue(CacheKey.Role, out HeroRole role)) {
				role = HeroRole.None;
			}

			return role;
		}
	}
	public class Selector : Select<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache> {
		public Selector(NodeList children) : base(children) {
		}
	}
	public class Sequence : Sequence<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache> {
		public Sequence(NodeList children) : base(children) {
		}
	}

	public class BehaviourTree : BehaviourTree<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache> {
		protected override BehaviourCache LocalCache { get; }

		public BehaviourTree() : base(new Selector(new NodeList {
			new HaveIAlreadyChosenCommand(),
			new GuaranteeAttackBehaviour(),
			new AttackEnemyBaseBehaviour(),
			new CreateBugBombBehaviour(),
			new OffensiveFarmMonstersBehaviour(),
			new ShieldMyselfBehaviour(),
			new DefendBaseFromMonstersBehaviour(),
			new DefensiveFarmMonstersBehaviour(),
			new PatrolBehaviour(),
			new WaitAction()
		})) {
			LocalCache = new BehaviourCache();
		}
	}
}
