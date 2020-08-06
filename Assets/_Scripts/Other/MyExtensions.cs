using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Hiragana.Other
{
	public static class MyExtensions
	{
		public static T GetRandom<T>(this List<T> list)
		{
			return list[Random.Range(0, list.Count)];
		}
	}
}
