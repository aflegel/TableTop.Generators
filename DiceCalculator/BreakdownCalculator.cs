using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceCalculator.Dice;

namespace DiceCalculator
{
	class BreakdownCalculator
	{
		//this is a decimal to do math
		decimal totalCount;


		public BreakdownCalculator(List<Die> testingDice)
		{
			List<Die> dicePool = testingDice;

			List<FaceMap> bulkPool = ProcessBulkPool(dicePool);
			totalCount = bulkPool.Count;

			Dictionary<FaceMap, int> simplifiedPool = ProcessSimplifiedPool(bulkPool);

			ProcessMainOutput(simplifiedPool);

			ProcessAnalysisOutput(simplifiedPool, dicePool);

			FaceMap test = Die.CountPool(dicePool);

		}

		/// <summary>
		/// Displays the Unique List of rolls
		/// </summary>
		/// <param name="simplifiedPool"></param>
		protected void ProcessMainOutput(Dictionary<FaceMap, int> simplifiedPool)
		{
			string format = "| {0,9:#0} | {1,11} | {2}";
			Console.WriteLine("Possible Roll Breakdown");
			Console.WriteLine("-----------------------");

			Console.WriteLine(string.Format(format, "Frequency", "Probability", "Definition"));

			foreach (KeyValuePair<FaceMap, int> map in simplifiedPool.OrderBy(x => x.Value))
			{
				//print roll count, probability, and the faces
				Console.WriteLine(string.Format(format, map.Value, (map.Value / totalCount).ToString("#0.0%"), map.Key.ToString()));
			}

			Console.WriteLine(string.Format("Total Possibilities: {0}", totalCount));
			Console.WriteLine(string.Format("Total Unique: {0}", simplifiedPool.Count));
		}


		/// <summary>
		/// Displays the breakdown of rolls and the probability of success
		/// </summary>
		/// <param name="simplifiedPool"></param>
		/// <param name="dicePool"></param>
		protected void ProcessAnalysisOutput(Dictionary<FaceMap, int> simplifiedPool, List<Die> dicePool)
		{
			//string format = "| {0,9} | {1,5:#0} | {2,9:#0} | {3,11:#0.0} |";
			Console.WriteLine();
			Console.WriteLine("Required Roll Breakdown");
			Console.WriteLine("-----------------------");
			//Console.WriteLine(string.Format(format, "Face", "Count", "Frequency", "Probability"));


			//var test = dicePool.SelectMany(x => x.faceMaps.Select(y => y.faces.Select(i => i.Key))).Distinct();

			foreach (Face face in Die.CountPool(dicePool).faces.Keys)
			{
				for (int i = 1; i <= dicePool.Count * 2; i++)
				{
					int found = SearchPool(simplifiedPool, new FaceMap(new Dictionary<Face, int>() { { face, i } }));

					//Console.WriteLine(string.Format(format, face.ToString(), i, found, (found / totalCount).ToString("#0.0%")));

					//don't bother continuing to process after no results
					if (found == 0)
						break;
				}
			}
		}

		/// <summary>
		/// Calculate all possible outcomes in a large dataset
		/// </summary>
		/// <returns></returns>
		protected List<FaceMap> ProcessBulkPool(List<Die> dicePool)
		{
			List<FaceMap> bulkPool = new List<FaceMap>();

			foreach (Die die in dicePool)
				bulkPool = Die.ProcessPool(bulkPool, die);

			return bulkPool;
		}

		/// <summary>
		/// Take a large dataset and refine into a unique list with frequency
		/// </summary>
		/// <param name="bulkPool"></param>
		/// <returns></returns>
		protected Dictionary<FaceMap, int> ProcessSimplifiedPool(List<FaceMap> bulkPool)
		{
			Dictionary<FaceMap, int> simplifiedPool = new Dictionary<FaceMap, int>();

			//go through the large pool and summarize into unique rolls
			foreach (FaceMap rolls in bulkPool)
			{
				if (simplifiedPool.ContainsKey(rolls))
					simplifiedPool[rolls] = simplifiedPool[rolls] + 1;
				else
					simplifiedPool.Add(rolls, 1);
			}

			return simplifiedPool;
		}


		/// <summary>
		/// Searches a pool of dice for a specific outcome and returns the number of rolls of that outcome
		/// </summary>
		/// <param name="simplifiedPool"></param>
		/// <param name="requiredQuery"></param>
		/// <returns></returns>
		protected int SearchPool(Dictionary<FaceMap, int> simplifiedPool, FaceMap requiredQuery)
		{
			//todo: This needs to be reconfigured to have a the ability to search for more than one field at a time
			string format = "| {0,9:#0} | {1,11} | {2}";

			Console.WriteLine(string.Format("{0}",  requiredQuery.ToString()));
			//FaceMap optionalQuery = new FaceMap(new Dictionary<Face, int>());
			//FaceMap requiredResults = new FaceMap(new Dictionary<Face, int>());
			//FaceMap optionalResults = new FaceMap(new Dictionary<Face, int>());

			int frequency = 0;

			int searchThreshold = requiredQuery.faces.Sum(x => x.Value);

			if (requiredQuery.faces.ContainsKey(Face.advantage))
			{
				//optionalQuery.faces.Add(Face.advantage, requiredQuery.faces[Face.advantage]);
				requiredQuery.faces.Add(Face.triumph, requiredQuery.faces[Face.advantage]);
			}
			else if (requiredQuery.faces.ContainsKey(Face.success))
			{
				//optionalQuery.faces.Add(Face.success, requiredQuery.faces[Face.success]);
				requiredQuery.faces.Add(Face.advantage, requiredQuery.faces[Face.success]);
				requiredQuery.faces.Add(Face.triumph, requiredQuery.faces[Face.success]);
			}


			if (requiredQuery.faces.ContainsKey(Face.threat))
			{
				//optionalQuery.faces.Add(Face.threat, requiredQuery.faces[Face.threat]);
				requiredQuery.faces.Add(Face.dispair, requiredQuery.faces[Face.threat]);
			}
			else if (requiredQuery.faces.ContainsKey(Face.failure))
			{
				//optionalQuery.faces.Add(Face.failure, requiredQuery.faces[Face.failure]);
				requiredQuery.faces.Add(Face.threat, requiredQuery.faces[Face.failure]);
				requiredQuery.faces.Add(Face.dispair, requiredQuery.faces[Face.failure]);
			}

			//			var selection = simplifiedPool.Select(x => x).Where(x => x.Key.faces.Keys.Intersect(query.faces.Keys).Any());
			//var searchKeys = requiredQuery.faces.Keys.Union(optionalQuery.faces.Keys);



			foreach (FaceMap map in simplifiedPool.Keys)
			{
				int threshold = 0;

				foreach (Face face in requiredQuery.faces.Keys)
				{
					if (map.faces.ContainsKey(face))
					{
						threshold += map.faces[face];
						//requiredResults.faces.Add(face, map.faces[face]);
					}
				}

				if (threshold == searchThreshold)
				{
					frequency += simplifiedPool[map];

					Console.WriteLine(string.Format(format, simplifiedPool[map], (simplifiedPool[map] / totalCount).ToString("#0.0%"), map.ToString()));
				}
			}

			Console.WriteLine(string.Format("Total {0} ({1}) ", frequency, (frequency / totalCount).ToString("#0.0%")));
			Console.WriteLine("---");

			return frequency;
		}
	}
}
