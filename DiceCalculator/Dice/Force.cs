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
			faces = new List<FaceMap>();

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dark, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dark, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dark, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dark, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dark, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dark, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dark, 2 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.light, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.light, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.light, 2 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.light, 2 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.light, 2 } }));
		}
	}
}
