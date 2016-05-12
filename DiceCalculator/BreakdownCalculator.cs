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

			ProcessPreOutput(dicePool);

			Dictionary<FaceMap, long> outcomePool = TestDicePool(dicePool);

			totalCount = outcomePool.Sum(s => s.Value);

			ProcessMainOutput(outcomePool);

			ProcessAnalysisOutput(outcomePool, dicePool);

			ProcessSuccessPool(outcomePool);

			FaceMap test = Die.CountPool(dicePool);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="dicePool"></param>
		protected void ProcessPreOutput(List<Die> dicePool)
		{
			var rollEstimation = dicePool.Aggregate((long)1, (x, y) => x * y.faceMaps.Count);

			Console.WriteLine(string.Format("Estimated number of outcomes: {0:n0}", rollEstimation));
		}

		protected Dictionary<FaceMap, long> TestDicePool(List<Die> dicePool)
		{

			Dictionary<FaceMap, long> bulkPool = new Dictionary<FaceMap, long>();

			List<Dictionary<FaceMap, long>> partialPools = new List<Dictionary<FaceMap, long>>();

			var groups = dicePool.Select((p, index) => new { p, index }).GroupBy(a => a.index / 6).Select((grp => grp.Select(g => g.p).ToList())).ToList();

			foreach (var group in groups)
			{
				partialPools.Add(ProcessDicePool(group));
			}

			foreach (var partial in partialPools)
			{
				Console.WriteLine(string.Format("Partial Rolls: {0,10:n0} Unique: {1,10:n0}", partial.Sum(s => s.Value), partial.Count));
				//gather cross product
				bulkPool = ProcessCrossProduct(bulkPool, partial);

				Console.WriteLine(string.Format("Progress Rolls: {0,10:n0} Unique: {1,10:n0}", bulkPool.Sum(s => s.Value), bulkPool.Count));
				Console.WriteLine("--");
			}

			Console.WriteLine(string.Format("Processed Rolls: {0,10:n0} Unique: {1,10:n0}", bulkPool.Sum(s => s.Value), bulkPool.Count));
			/*
			var test = ProcessDicePool(dicePool);

			Console.WriteLine(string.Format("Confirming Rolls: {0,10:n0} Unique: {1,10:n0}", test.Sum(s => (long)s.Value), test.Count));
			*/
			return bulkPool;
		}


		protected Dictionary<FaceMap, long> ProcessDicePool(List<Die> dicePool)
		{
			int[] indexTracker = new int[dicePool.Count];
			for (int i = 0; i < dicePool.Count; i++)
				indexTracker[i] = 0;

			Dictionary<FaceMap, long> bulkPool = new Dictionary<FaceMap, long>();

			while (indexTracker[dicePool.Count - 1] < dicePool[dicePool.Count - 1].faceMaps.Count)
			{
				for (int i = 0; i < dicePool[0].faceMaps.Count; i++)
				{
					//add the zero index to the mix
					FaceMap node = dicePool[0].faceMaps[i];
					//Console.WriteLine(string.Format("Die: {0} Face: {1}", 0, i));

					for (int j = 1; j < dicePool.Count; j++)
					{
						node = node.Merge(dicePool[j].faceMaps[indexTracker[j]]);

						//Console.WriteLine(string.Format("Die: {0} Face: {1}", j, indexTracker[j]));
					}

					//Console.WriteLine("---");

					//add the node to the mix
					if (node.faces.Count > 0)
					{

						try
						{
							bulkPool[node] = bulkPool[node] + 1;
						}
						catch
						{
							bulkPool.Add(node, 1);
						}
					}
				}

				//manually update the next index
				if (dicePool.Count > 1)
					indexTracker[1]++;
				else
					indexTracker[0] = dicePool[0].faceMaps.Count;

				//update the indexes
				for (int i = 1; i < dicePool.Count; i++)
				{
					if (indexTracker[i] >= dicePool[i].faceMaps.Count)
					{
						if (i > 6)
						{
							Console.WriteLine();
							Console.WriteLine(string.Format("Processing: {0:n0}", bulkPool.Sum(s => s.Value)));
						}
						else if (i > 4)
							Console.Write(".");

						if (i < dicePool.Count - 1)
						{
							indexTracker[i] = 0;
							indexTracker[i + 1]++;
						}
						else
						{
							Console.WriteLine();
						}
					}
				}
			}





			return bulkPool;
		}

		protected Dictionary<FaceMap, long> ProcessCrossProduct(Dictionary<FaceMap, long> startingPool, Dictionary<FaceMap, long> additionalPool)
		{

			if (startingPool.Count == 0)
				return additionalPool;

			Dictionary<FaceMap, long> bulkPool = new Dictionary<FaceMap, long>();

			foreach (FaceMap startingMap in startingPool.Keys)
			{
				foreach (FaceMap addingMap in additionalPool.Keys)
				{
					FaceMap node = startingMap.Merge(addingMap);
					long combinedFrequency = startingPool[startingMap] * additionalPool[addingMap];

					try
					{
						bulkPool[node] = bulkPool[node] + combinedFrequency;
					}
					catch
					{
						bulkPool.Add(node, combinedFrequency);
					}
				}
			}

			return bulkPool;
		}


		/// <summary>
		/// Displays the Unique List of rolls
		/// </summary>
		/// <param name="outcomePool"></param>
		protected void ProcessMainOutput(Dictionary<FaceMap, long> outcomePool)
		{
			string format = "| {0,9:#0} | {1,11} | {2}";
			Console.WriteLine("Possible Roll Breakdown");
			Console.WriteLine("-----------------------");

			/*
			 *
			 * Console.WriteLine(string.Format(format, "Frequency", "Probability", "Definition"));

			foreach (KeyValuePair<FaceMap, int> map in outcomePool.OrderBy(x => x.Value))
			{
				//print roll count, probability, and the faces
				Console.WriteLine(string.Format(format, map.Value, (map.Value / totalCount).ToString("#0.0%"), map.Key.ToString()));
			}*/

			Console.WriteLine(string.Format("Total Possibilities: {0:n0}", totalCount));
			Console.WriteLine(string.Format("Total Unique: {0:n0}", outcomePool.Count));
		}


		/// <summary>
		/// Displays the breakdown of rolls and the probability of success
		/// </summary>
		/// <param name="outcomePool"></param>
		/// <param name="dicePool"></param>
		protected void ProcessAnalysisOutput(Dictionary<FaceMap, long> outcomePool, List<Die> dicePool)
		{
			//string format = "| {0,9} | {1,5:#0} | {2,9:#0} | {3,11:#0.0} |";
			Console.WriteLine();
			Console.WriteLine("Required Roll Breakdown");
			Console.WriteLine("-----------------------");
			//Console.WriteLine(string.Format(format, "Face", "Count", "Frequency", "Probability"));


			//var test = dicePool.SelectMany(x => x.faceMaps.Select(y => y.faces.Select(i => i.Key))).Distinct();

			foreach (Face face in Die.CountPool(dicePool).faces.Keys)
			{
				for (byte i = 1; i <= dicePool.Count * 2; i++)
				{
					long found = ProcessBreakdownPool(outcomePool, new FaceMap(new Dictionary<Face, byte>() { { face, i } }));

					//Console.WriteLine(string.Format(format, face.ToString(), i, found, (found / totalCount).ToString("#0.0%")));

					//don't bother continuing to process after no results
					if (found == 0)
						break;
				}
			}
		}

		/// <summary>
		/// Searches a pool of dice for a specific outcome and returns the number of rolls of that outcome
		/// </summary>
		/// <param name="outcomePool"></param>
		/// <param name="requiredQuery"></param>
		/// <returns></returns>
		protected long ProcessBreakdownPool(Dictionary<FaceMap, long> outcomePool, FaceMap requiredQuery)
		{
			//todo: This needs to be reconfigured to have a the ability to search for more than one field at a time
			string format = "| {0,9:#0} | {1,11} | {2}";

			Console.WriteLine(string.Format("{0}", requiredQuery.ToString()));

			//initialize the frequency for this requirement and the threshold for match
			long frequency = 0;
			int searchThreshold = requiredQuery.faces.Sum(x => x.Value);

			//expand the search to include the superior versions of the requirement
			if (requiredQuery.faces.ContainsKey(Face.advantage))
			{
				requiredQuery.faces.Add(Face.triumph, requiredQuery.faces[Face.advantage]);
			}
			else if (requiredQuery.faces.ContainsKey(Face.success))
			{
				requiredQuery.faces.Add(Face.advantage, requiredQuery.faces[Face.success]);
				requiredQuery.faces.Add(Face.triumph, requiredQuery.faces[Face.success]);
			}

			if (requiredQuery.faces.ContainsKey(Face.threat))
			{
				requiredQuery.faces.Add(Face.dispair, requiredQuery.faces[Face.threat]);
			}
			else if (requiredQuery.faces.ContainsKey(Face.failure))
			{
				requiredQuery.faces.Add(Face.threat, requiredQuery.faces[Face.failure]);
				requiredQuery.faces.Add(Face.dispair, requiredQuery.faces[Face.failure]);
			}

			//loop through the simple pool to find matches
			foreach (FaceMap map in outcomePool.Keys)
			{
				int threshold = 0;

				foreach (Face face in requiredQuery.faces.Keys)
				{
					//if it finds a matching key increase the threshold
					if (map.faces.ContainsKey(face))
						threshold += map.faces[face];
				}

				//if the found threshold is the same as the required threshold add the frequency and display the roll result
				if (threshold == searchThreshold)
				{
					frequency += outcomePool[map];
					//Console.WriteLine(string.Format(format, outcomePool[map], (outcomePool[map] / totalCount).ToString("#0.0%"), map.ToString()));
				}
			}

			Console.WriteLine(string.Format("Total {0:n0} ({1}) ", frequency, (frequency / totalCount).ToString("#0.000%")));
			Console.WriteLine("---");

			return frequency;
		}

		protected void ProcessSuccessPool(Dictionary<FaceMap, long> outcomePool)
		{
			string format = "| {0,9:#0} | {1,11} | {2}";

			Console.WriteLine("Breakdown of Successes");
			Console.WriteLine("----------------------");

			List<Face> successKeys = new List<Face>() { Face.success, Face.advantage, Face.triumph };
			List<Face> failureKeys = new List<Face>() { Face.failure, Face.threat, Face.dispair };
			long successFrequency = 0;

			//loop through the simple pool to find matches
			foreach (FaceMap map in outcomePool.Keys)
			{
				int successThreshold = 0;
				int failureThreshold = 0;

				foreach (Face face in successKeys)
				{
					//if it finds a matching key increase the threshold
					if (map.faces.ContainsKey(face))
						successThreshold += map.faces[face];
				}

				foreach (Face face in failureKeys)
				{
					//if it finds a matching key increase the threshold
					if (map.faces.ContainsKey(face))
						failureThreshold += map.faces[face];
				}

				//if the found threshold is the same as the required threshold add the frequency and display the roll result
				if (successThreshold > 0 && successThreshold > failureThreshold)
				{
					successFrequency += outcomePool[map];
					//Console.WriteLine(string.Format(format, outcomePool[map], (outcomePool[map] / totalCount).ToString("#0.0%"), map.ToString()));
				}
			}

			Console.WriteLine(string.Format("Successes: {0:n0} ({1}) ", successFrequency, (successFrequency / totalCount).ToString("#0.0000%")));
			Console.WriteLine(string.Format("Failures:  {0:n0} ({1}) ", totalCount - successFrequency, ((totalCount - successFrequency) / totalCount).ToString("#0.0000%")));
			Console.WriteLine("---");
		}
	}
}
