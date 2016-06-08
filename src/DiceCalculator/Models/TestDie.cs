using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{
	class TestDie : Die
	{
		public TestDie()
		{
			faceMaps = new List<FaceMap>();

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.blank, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.success, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.success, 2 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.blank, 2 } }));

		}
	}
}
