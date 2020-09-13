using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.Effects
{
	public class DirectDamage : Effect
	{
		[SerializeField] private int value = 5;

		public DirectDamage()
		{

		}

		public DirectDamage(Romaji romaji)
		{
			value = (int)romaji;
		}

		public DirectDamage(int value)
		{
			this.value = value;
		}

		private DirectDamage(DirectDamage org)
		{
			value = org.value;
		}

		public override Effect Clone()
		{
			return new DirectDamage(this);
		}

		public override void Apply(IBattleTarget target)
		{
			if (target is Player player)
			{
				player.Health -= value;
			}
			else
			{
				var enemy = target as Enemy;
				Romaji damage = (Romaji)value;
				LifeSegment life = enemy.Health.FirstOrDefault(s => s.Romaji == damage);
				if (life != null) // hit
				{
					life.damaged = true;
				}
			}
		}
	}
}