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
		TargetLocation,
		TargetEntityNumber,
		IgnoredEntities,
		WaitingHeroes
	}

	public class BehaviourCache : Dictionary<CacheKey, object> {
		public bool TryGetValue<T>(CacheKey key, out T value) {
			bool result = TryGetValue(key, out var dynamicValue);
			value = dynamicValue is T typedValue ? typedValue : default;
			return result;
		}
	}

	public class NodeList : List<AbstractNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache>> {}

	public class Inverter : Inverter<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
		public Inverter(AbstractNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> children) : base(children) {
		}
	}

	public abstract class Leaf : Leaf<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
		protected HeroRole GetRoleOrDefault(BehaviourCache cache) {
			if (!cache.TryGetValue(CacheKey.Role, out HeroRole role)) {
				role = HeroRole.None;
			}

			return role;
		}
	}
	public class Selector : Select<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
		public Selector(NodeList children) : base(children) {
		}
	}
	public class Sequence : Sequence<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
		public Sequence(NodeList children) : base(children) {
		}
	}

	public class RepeaterUntilFail : RepeaterUntilFail<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
		public RepeaterUntilFail(int repetitions, AbstractNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> children) : base(repetitions, children) {
		}
	}

	public class RepeaterUntilSuccess : RepeaterUntilSuccess<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
		public RepeaterUntilSuccess(int repetitions, AbstractNode<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> children) : base(repetitions, children) {
		}
	}

	public class BehaviourTree : BehaviourTree<Hero, State, IDictionary<Hero, AbstractCommand>, BehaviourCache, BehaviourCache> {
		protected override BehaviourCache LocalCache { get; }
		protected override BehaviourCache GlobalCache { get; }

		public BehaviourTree() : base(new Selector(new NodeList {
			new HaveIAlreadyChosenCommand(),
			new DefendBaseFromMonstersBehaviour(),
			new DefensiveFarmMonstersBehaviour(),
			new AttackEnemyBaseBehaviour(),
			new CreateBugBombBehaviour(),
			new EscortBugBombBehaviour(),
			new OffensiveFarmMonstersBehaviour(),
			new MidfielderFarmMonstersBehaviour(),
			new WaitAction()
		})) {
			LocalCache = new BehaviourCache();
			GlobalCache = new BehaviourCache();
		}
	}
}
