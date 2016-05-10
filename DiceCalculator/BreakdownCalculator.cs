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
		List<Die> dicePool;
		decimal totalCount;


		public BreakdownCalculator(List<Die> testingDice)
		{
			dicePool = testingDice;

			List<FaceMap> bulkPool = ProcessBulkPool();
			totalCount = bulkPool.Count;

			Dictionary<FaceMap, int> simplifiedPool = ProcessSimplifiedPool(bulkPool);

			ProcessMainOutput(simplifiedPool);

			ProcessAnalysisOutput(simplifiedPool);

			FaceMap test = Die.CountPool(dicePool);

		}

		protected void ProcessMainOutput(Dictionary<FaceMap, int> simplifiedPool)
		{
			string format = "| {0,9:#0} | {1,11} | {2}";
			Console.WriteLine("Possible Roll Breakdown");
			Console.WriteLine("-----------------------");

			Console.WriteLine(string.Format(format, "Frequency", "Probability", "Definition"));

			foreach (KeyValuePair<FaceMap, int> map in simplifiedPool.OrderBy(x => x.Value))
			{
				//print roll count, probability, and the faces
				Console.WriteLine(string.Format(format, map.Value, (map.Value / totalCount * 100).ToString("#0.0") + "%", map.Key.ToString()));
			}

			Console.WriteLine(string.Format("Total Possibilities: {0}", totalCount));
			Console.WriteLine(string.Format("Total Unique: {0}", simplifiedPool.Count));
		}

		protected void ProcessAnalysisOutput(Dictionary<FaceMap, int> simplifiedPool)
		{
			string format = "| {0,9} | {1,7:#0} | {2,9:#0} | {3,11:#0.0} |";
			Console.WriteLine();
			Console.WriteLine("Required Roll Breakdown");
			Console.WriteLine("-----------------------");
			Console.WriteLine(string.Format(format, "Face", "Minimum", "Frequency", "Probability"));


			var test = dicePool.SelectMany(x => x.faceMaps.Select(y => y.faces.Select(i => i.Key))).Distinct();

			foreach (Face face in Die.CountPool(dicePool).faces.Keys)
			{
				for (int i = 1; i <= dicePool.Count * 2; i++)
				{
					int found = SearchPool(simplifiedPool, new FaceMap(new Dictionary<Face, int>() { { face, i } }), 0);
					Console.WriteLine(string.Format(format, face.ToString(), i, found, (found / totalCount * 100).ToString("#0.0") + "%"));

					if (found == 0)
						break;
				}
			}
		}

		/// <summary>
		/// Calculate all possible outcomes in a large dataset
		/// </summary>
		/// <returns></returns>
		protected List<FaceMap> ProcessBulkPool()
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

		protected int SearchPool(Dictionary<FaceMap, int> simplifiedPool, FaceMap query, int searchThreshold)
		{
			int frequency = 0;

			searchThreshold = query.faces.Sum(x => x.Value);

			if (query.faces.ContainsKey(Face.advantage))
				query.faces.Add(Face.triumph, query.faces[Face.advantage]);
			else if (query.faces.ContainsKey(Face.success))
			{
				query.faces.Add(Face.advantage, query.faces[Face.success]);
				query.faces.Add(Face.triumph, query.faces[Face.success]);
			}


			if (query.faces.ContainsKey(Face.threat))
				query.faces.Add(Face.dispair, query.faces[Face.success]);
			else if (query.faces.ContainsKey(Face.failure))
			{
				query.faces.Add(Face.threat, query.faces[Face.success]);
				query.faces.Add(Face.dispair, query.faces[Face.success]);
			}

			//			var selection = simplifiedPool.Select(x => x).Where(x => x.Key.faces.Keys.Intersect(query.faces.Keys).Any());


			foreach (FaceMap map in simplifiedPool.Keys)
			{
				int threshold = 0;

				foreach (Face face in query.faces.Keys)
				{
					if (map.faces.ContainsKey(face))
					{
						threshold += map.faces[face];
					}

					if (threshold >= searchThreshold)
					{
						frequency += simplifiedPool[map];
						break;
					}
				}
			}


			return frequency;
		}
	}
}
