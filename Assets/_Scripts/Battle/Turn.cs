using System.Collections;
using UnityEngine;

namespace Hiragana.Battle
{
	public abstract class Turn
	{
		public abstract IBattleTarget Target { get; }

		public abstract IEnumerator Execute();

		public void Recalculate()
		{
			Target.TurnProgress += Target.Speed;
		}

		public bool Active()
		{
			if (Target.TurnProgress >= 100)
			{
				Target.TurnProgress -= 100;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
