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
			faces = new List<FaceMap>();

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.blank, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.success, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.success, 2 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.blank, 2 } }));

		}
	}
}
