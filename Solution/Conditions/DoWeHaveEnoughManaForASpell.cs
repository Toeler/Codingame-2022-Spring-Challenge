using Codingame_2022_Spring_Challenge.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Codingame_2022_Spring_Challenge.Conditions {
	public class DoWeHaveEnoughMana: Leaf {
		private int ReserveMana { get; }
		protected int RequiredMana { get; }
		public DoWeHaveEnoughMana(int requiredMana, int reserveMana) {
			ReserveMana = reserveMana;
			RequiredMana = requiredMana;
		}

		public override bool Execute(Hero entity, State state, IDictionary<Hero, AbstractCommand> chosenCommands, BehaviourCache cache) {
			int numberOfSpellsThisTurn = chosenCommands.Count(command => command.Value is UseSpellCommand);
			int availableMana = state.MyMana - numberOfSpellsThisTurn * InitialState.SpellManaCost;

			return availableMana - ReserveMana >= RequiredMana;
		}
	}

	public class DoWeHaveEnoughManaForASpell : DoWeHaveEnoughMana {
		public DoWeHaveEnoughManaForASpell(int reserveMana): base(InitialState.SpellManaCost, reserveMana) {
		}
	}
}
