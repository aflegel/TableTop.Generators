using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{
	class Difficulty : Die
	{

		public Difficulty()
		{
			faceMaps = new List<FaceMap>();

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.blank, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 2 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 }, { Face.threat, 1 } }));
		}
	}
}
