using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hiragana.Other
{
	public static class MyExtensions
	{
		public static T TryGetRandom<T>(this List<T> list)
		{
			try
			{
				return list[Random.Range(0, list.Count)];
			}
			catch { return default; }
		}
	}
}
