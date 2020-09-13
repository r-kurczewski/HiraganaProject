using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hiragana
{
	public class SpeedTest : MonoBehaviour
	{
		void Start()
		{
			List<Turn> turns = new List<Turn>()
			{
				new Turn(15, "A"),
				new Turn(13, "B"),
				new Turn(12, "C"),
				new Turn(10, "D"),
				new Turn(8, "E"),
			};

			Dictionary<string, int> dict = new Dictionary<string, int>();
			foreach(var turn in turns)
			{
				dict[turn.name] = 0;
			}

			turns.OrderBy(x => x.speed);

			for (int i = 0; i < 100; i++)
			{
				foreach (var turn in turns)
				{
					if (turn.HaveTurn())
					{
						Debug.Log(turn.name);
						dict[turn.name]++;
					}
				}
			}
			foreach(var pair in dict)
			{
				Debug.Log($"{pair.Key}: {pair.Value}");
			}
		}

		class Turn
		{
			public int speed;
			public string name;
			int modulo;
			int step = 0;

			public Turn(int speed, string name)
			{
				this.speed = speed;
				this.name = name;
				modulo = 100 / speed;
			}

			public bool HaveTurn()
			{
				step = (step + 1) % modulo;
				return step == 0;
			}
		}

	}
}