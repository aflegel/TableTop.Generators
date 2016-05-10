using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceCalculator.Dice;

namespace DiceCalculator
{
	class Program
	{
		static void Main(string[] args)
		{

			List<Die> Dice = new List<Die>();
			List<FaceMap> bulkPool = new List<FaceMap>();
			Dictionary<FaceMap, int> simplifiedPool = new Dictionary<FaceMap, int>();

			Dice.Add(new TestDie());
			Dice.Add(new TestDie());

			//loop through all the possibilities and create a single large pool
			foreach (Die die in Dice)
			{
				bulkPool = Die.ProcessPool(bulkPool, die);
			}

			var stop = 0;

			//go through the large pool and summarize into unique rolls
			foreach (FaceMap rolls in bulkPool)
			{
				if (simplifiedPool.ContainsKey(rolls))
					simplifiedPool[rolls] = simplifiedPool[rolls] + 1;
				else
					simplifiedPool.Add(rolls, 1);
			}

			stop = 1;



			Console.WriteLine("Hello World!");
			var ordered = simplifiedPool.OrderBy(x => x.Value);
			foreach ( KeyValuePair<FaceMap, int> map in ordered)
			{
				//print roll count, probability, and the faces
				Console.WriteLine( string.Format("{0,5:###0} ({1,4:#0.0}%) - {2}", map.Value, map.Value / (decimal)bulkPool.Count * 100 , map.Key.ToString()));
			}

			//todo: gather total face counts

			FaceMap test = Die.CountPool(Dice);

			//todo: use face counts to figure out probabilities of total outcomes

			stop = 2;
		}
	}
}
