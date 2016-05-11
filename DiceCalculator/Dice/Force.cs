using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{
	class Force : Die
	{
		public Force()
		{
			faceMaps = new List<FaceMap>();

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.dark, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.dark, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.dark, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.dark, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.dark, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.dark, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.dark, 2 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.light, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.light, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.light, 2 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.light, 2 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.light, 2 } }));
		}
	}
}
