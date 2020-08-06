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
		[SerializeField] private int value;

		public Heal()
		{

		}

		public Heal(int value)
		{
			this.value = value;
		}

		public Heal(Romaji romaji)
		{
			value = (int)romaji;
		}

		public override bool Apply(IBattleTarget target)
		{
			if (target is Player player)
			{
				player.Health += value;
				return true;
			}
			else
			{
				var enemy = target as Enemy;
				bool hit = false;
				for (int i = 0; i < value; i++)
				{
					LifeSegment life = enemy.Health.FirstOrDefault(l => l.damaged);
					if (life != null)
					{
						hit = true;
						life.damaged = false;
					}
					else
					{
						return hit;
					}
				}
				return hit;
			}
		}
	}
}