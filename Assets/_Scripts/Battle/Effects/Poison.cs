using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	[Serializable]
	public class Poison : IStatus
	{
		private IBattleTarget target;
		private int value;
		private int turns;

		public Poison(IBattleTarget target, int value, int turns)
		{
			this.target = target;
			this.value = value;
			this.turns = turns;
		}

		public void Execute()
		{
			if (turns > 0)
			{
				target.ApplyDamage(value);
				turns--;
			}
			else
			{
				target.RemoveStatus(this);
			}
		}
	}
}