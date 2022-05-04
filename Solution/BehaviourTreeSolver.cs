using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codingame_2022_Spring_Challenge.Commands;
using Lib;

namespace Codingame_2022_Spring_Challenge {
	public class CommandDictionary : Dictionary<Hero, AbstractCommand> {
		public override string ToString() {
			return string.Join("|", Keys.Select(key => $"{key.Id}:{this[key]} {this[key].Role}"));
		}
	}

	public class BehaviourTreeSolver : ISolver<State, SingleMoveSolution<IDictionary<Hero, AbstractCommand>>> {
		public string ShortName => "BT";

		public IEnumerable<SingleMoveSolution<IDictionary<Hero, AbstractCommand>>> GetSolutions(State problem, Timer timer) {
			CommandDictionary chosenCommands = new CommandDictionary();
			var behaviourTree = new BehaviourTree();
			var lastIterationHeroesWithoutMoves = new List<Hero>();
			while (!timer.IsFinished() && chosenCommands.Keys.Count != problem.MyHeroes.Length) {
				foreach (var hero in problem.MyHeroes) {
#if DEBUG
					Console.Error.WriteLine($"Hero {hero.Id}");
#endif
					behaviourTree.Execute(hero, problem, chosenCommands);
				}


				var heroesWithoutMoves = problem.MyHeroes.Except(chosenCommands.Keys).ToList();
				if (heroesWithoutMoves.Count > 0 && heroesWithoutMoves.Count == lastIterationHeroesWithoutMoves.Count) {
					// We're stuck, both heroes want the other to choose first. Sub-optimally, the first hero can miss out
					Console.Error.WriteLine($"Heroes with no move picked: {string.Join(", ", heroesWithoutMoves.Select(h => h.Id))}. Hero {heroesWithoutMoves.First().Id} Waiting.");
					chosenCommands.Add(heroesWithoutMoves.First(), new WaitCommand(HeroRole.None));
				}

				lastIterationHeroesWithoutMoves = heroesWithoutMoves;
			}

			return new [] { new SingleMoveSolution<IDictionary<Hero, AbstractCommand>>(chosenCommands, 1) };
		}
	}
}
