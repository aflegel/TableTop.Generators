using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DiceCalculator.Dice;
using System.Web.Mvc;

namespace DiceCalculator
{
	class Program
	{
		const int ABILITY_LIMIT = 6;
		const int UPGRADE_LIMIT = 4;
		const int DIFFICULTY_LIMIT = 6;
		const int CHALLENGE_LIMIT = 4;

		static void Main(string[] args)
		{
			//LargeTest();
			//MatchTest();
			SkillBreakdown();
		}

		/// <summary>
		/// Runs a series of tests to calculate the total output
		/// </summary>
		public static void SkillBreakdown()
		{
			StreamWriter writer = new StreamWriter("DiceResults.csv");

			List<DieResult> resultList = new List<DieResult>();

			//each ability level
			for (int i = 1; i <= ABILITY_LIMIT; i++)
			{
				//each skill level
				//ensure the proficiency dice don't outweigh the ability dice
				for (int j = 0; (j <= UPGRADE_LIMIT) && (j <= i); j++)
				{
					//each difficulty
					for (int k = 1; k <= DIFFICULTY_LIMIT; k++)
					{
						//ensure the proficiency dice don't outweigh the ability dice
						for (int l = 0; (l <= CHALLENGE_LIMIT) && (l <= k); l++)
						{
							List<Die> pool = GetPool(i - j, j, k - l, l);
							BreakdownCalculator calculator = new BreakdownCalculator(pool);
							resultList.Add(calculator.Run());
						}
					}
				}
			}

			writer.WriteLine("pool, total, successes, failures, advantages, threats, stalemate, triumphs, despairs");
			writer.WriteLine(string.Format("{0}", string.Join("\n", resultList)));

			writer.Close();
		}

		protected static List<Die> GetPool(int ability, int proficiency, int difficulty, int challenge)
		{
			List<Die> testPool = new List<Die>();

			for (int i = 0; i < ability; i++)
				testPool.Add(new Ability());

			for (int i = 0; i < proficiency; i++)
				testPool.Add(new Proficiency());

			for (int i = 0; i < difficulty; i++)
				testPool.Add(new Difficulty());

			for (int i = 0; i < challenge; i++)
				testPool.Add(new Challenge());

			var poolText = testPool.GroupBy(info => info.ToString()).Select(group => string.Format("{0} {1}", group.Key, group.Count())).ToList();
			Console.WriteLine(string.Format("Pool: {0}", string.Join(", ", poolText)));

			return testPool;
		}

		public static void LargeTest()
		{
			BreakdownCalculator diceCalculator = new BreakdownCalculator(new List<Die>()
			{
				new Boost(),
				new Boost(),
				new Boost(),
				new Boost(),
				new Boost(),//5
				new Ability(),
				new Ability(),
				new Difficulty(),
				new Difficulty(),
				new Difficulty(),//10
				new Difficulty(),
				new SetBack(),
				new SetBack(),
				new SetBack(),
				new SetBack()
				});
		}


		public static void MatchTest()
		{
			BreakdownCalculator diceCalculator = new BreakdownCalculator(new List<Die>()
			{
				//new Boost(),
				new Ability(),
				new Ability(),
				new Ability(),
				new Ability(),
				//new Proficiency(),
				//new Ability(),
				//new Ability(),
				//new Difficulty(),
				//new Difficulty(),
				new Difficulty(),
				new Difficulty(),
				new Difficulty(),
				new Difficulty()
			});
		}
	}
}
