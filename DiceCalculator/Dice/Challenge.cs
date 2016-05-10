﻿using System;
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
			faces = new List<FaceMap>();

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.blank, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 }, { Face.threat, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.failure, 1 }, { Face.threat, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.threat, 1 } }));

			faces.Add(new FaceMap(new Dictionary<Face, int>() { { Face.dispair, 1 } }));
		}
	}
}
