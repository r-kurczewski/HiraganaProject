using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.Effects
{
	public class Damage : Effect
	{
		[SerializeField] private int value;

		public Damage()
		{

		}

		public Damage(int value)
		{
			this.value = value;
		}

		public Damage(Romaji romaji)
		{
			value = (int)romaji;
		}

		public override bool Apply(IBattleTarget target)
		{
			if (target is Player player)
			{
				player.Health -= value;
				return true;
			}
			else
			{
				var enemy = target as Enemy;
				LifeSegment life = enemy.Health.FirstOrDefault(s => s.Romaji == (Romaji)value);
				if (life != null)
				{
					bool hit = !life.damaged;
					life.damaged = true;
					return hit;
				}
				else
				{
					return false;
				}
			}
		}
	}
}