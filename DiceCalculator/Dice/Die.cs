using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{

	/// <summary>
	/// A Base class for the child dice with some static counting functions
	/// </summary>
	class Die
	{

		public List<FaceMap> faceMaps;

		/// <summary>
		///
		/// </summary>
		/// <param name="pool"></param>
		/// <param name="addition"></param>
		/// <returns></returns>
		public static List<FaceMap> ProcessPool(List<FaceMap> pool, Die addition)
		{
			//escape for new lists
			if (pool.Count == 0)
				return addition.faceMaps;

			List<FaceMap> nextPool = new List<FaceMap>();

			foreach (FaceMap dieFaces in addition.faceMaps)
			{
				foreach (FaceMap poolFaces in pool)
				{
					nextPool.Add(poolFaces.Merge(dieFaces));
				}
			}

			return nextPool;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="pool"></param>
		/// <returns></returns>
		public static FaceMap CountPool(List<Die> pool)
		{
			//escape for new lists
			if (pool.Count == 0)
				return new FaceMap(new Dictionary<Face, int>());

			FaceMap nextPool = new FaceMap(new Dictionary<Face, int>());

			foreach (Die dieFaces in pool)
			{
				foreach (FaceMap face in dieFaces.faceMaps)
				{
					nextPool = face.Merge(nextPool);
				}
			}

			return nextPool;
		}

	}
}
