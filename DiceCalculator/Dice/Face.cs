using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceCalculator.Dice
{
	/// <summary>
	/// An Enum to capture the different kind of faces
	/// </summary>
	public enum Face
	{
		blank = 0,
		success = 1,
		failure = 2,
		advantage = 3,
		threat = 4,
		triumph = 5,
		despair = 6,
		light = 7,
		dark = 8
	}

	public struct DieResult
	{
		public override string ToString()
		{
			return string.Format("\"{0}\", {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}", dice, count, success, failure, advantage, threat, stalemate, triumph, despair);
		}

		public string dice;
		public long count;
		public long success;
		public long failure;
		public long advantage;
		public long threat;
		public long stalemate;
		public long triumph;
		public long despair;
	}

	public struct FaceMap
	{
		public Dictionary<Face, byte> faces;
		public FaceMap(Dictionary<Face, byte> keys)
		{
			faces = keys;
		}

		public override int GetHashCode()
		{
			//gets a unique has by summing the hash of the string and a hash of the value
			return faces.Sum(x => x.Key.GetHashCode() + x.Value.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(FaceMap))
				return false;

			if (((FaceMap)obj).faces.Count != faces.Count)
				return false;

			var keys = ((FaceMap)obj).faces.Keys.Union(faces.Keys);

			foreach (Face key in keys)
			{
				if (!(((FaceMap)obj).faces.ContainsKey(key) && faces.ContainsKey(key)))
					return false;
				else if (((FaceMap)obj).faces[key] != faces[key])
					return false;
			}

			return true;
		}

		/// <summary>
		/// Returns a summarized set of Faces
		/// </summary>
		/// <param name="firstSet"></param>
		/// <returns></returns>
		public FaceMap Merge(FaceMap firstSet)
		{
			Dictionary<Face, byte> merged = new Dictionary<Face, byte>();

			var keys = firstSet.faces.Keys.Union(faces.Keys);

			foreach (Face key in keys)
			{
				if (firstSet.faces.ContainsKey(key))
				{
					if (faces.ContainsKey(key))
						merged.Add(key, (byte)(firstSet.faces[key] + faces[key]));
					else
						merged.Add(key, firstSet.faces[key]);
				}
				else
				{
					merged.Add(key, faces[key]);
				}
			}

			return new FaceMap(merged);
		}

		public override string ToString()
		{
			var ordered = faces.OrderBy(x => x.Key.ToString());

			return string.Join(", ", ordered.Select(x => string.Format("{0,9} ({1})", x.Key.ToString(), x.Value)));
		}
	}
}
