using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{
	class SetBack : Die
	{

		public SetBack()
		{
			faceMaps = new List<FaceMap>();

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.blank, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.blank, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faceMaps.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));
		}
	}
}
