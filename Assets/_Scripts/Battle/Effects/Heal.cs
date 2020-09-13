using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.Effects
{
	[Serializable]
	public class Heal : Effect
	{
		[SerializeField] private int value = 50;

		public Heal()
		{

		}

		public Heal(int value)
		{
			this.value = value;
		}

		private Heal(Heal org)
		{
			value = org.value;
		}

		public override void Apply(IBattleTarget target)
		{
			if (target is Player player)
			{
				player.Health += value;
			}
			else
			{
				var enemy = target as Enemy;
				for (int i = 0; i < value; i++)
				{
					LifeSegment life = enemy.Health.FirstOrDefault(l => l.damaged);
					if (life != null) life.damaged = false;
				}
			}
		}

		public override Effect Clone()
		{
			return new Heal(this);
		}
	}
}