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
		[SerializeField] private int value = 5;

		public Damage()
		{

		}

		public Damage(Romaji romaji)
		{
			value = (int)romaji;
		}

		public Damage(int value)
		{
			this.value = value;
		}

		private Damage(Damage org)
		{
			value = org.value;
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
				if (life != null)
				{
					life.damaged = true;
					life.status?.OnHit();
				}
			}
		}



		public override Effect Clone()
		{
			return new Damage(this);
		}
	}
}