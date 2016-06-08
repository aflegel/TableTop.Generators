using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{
	class Boost : Die
	{
		public Boost()
		{
			faceMaps = new List<FaceMap>();

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.blank, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.blank, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.success, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.success, 1 }, { Face.advantage, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.advantage, 2 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.advantage, 1 } }));

		}
	}
}
