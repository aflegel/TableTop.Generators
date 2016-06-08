using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{
	class Challenge : Die
	{
		public Challenge()
		{
			faceMaps = new List<FaceMap>();

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.blank, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.failure, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.failure, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.failure, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.failure, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.failure, 1 }, { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.failure, 1 }, { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, byte>() { { Face.despair, 1 } }));
		}
	}
}
