
using System.Linq;
using UnityEngine;
using static Hiragana.Battle.Enemy;
using static Hiragana.Other.MyExtensions;

namespace Hiragana.Battle
{
	public abstract class HiraganaStatus : Status
	{
		protected LifeSegment life;

		protected HiraganaStatus()
		{

		}

		public HiraganaStatus(LifeSegment life)
		{
			this.life = life;
		}

		public abstract string GetStatusFormating(string str);

		public override bool Apply(IBattleTarget target)
		{
			var enemy = target as Enemy;
			LifeSegment life = enemy.Health.GetRandom();
			if(life != null)
			{
				life.status = this;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}