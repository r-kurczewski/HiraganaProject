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
		bool gainFocus = true;

		public Damage()
		{

		}

		public Damage(Romaji romaji, bool gainFocus = true)
		{
			value = (int)romaji;
			this.gainFocus = gainFocus;
		}

		public Damage(int value)
		{
			this.value = value;
		}

		private Damage(Damage org)
		{
			value = org.value;
			gainFocus = org.gainFocus;
		}

		public override Effect Clone()
		{
			return new Damage(this);
		}

		public override void Apply(IBattleTarget target)
		{
			if (target is Player player)
			{
				player.Health -= (int)Mathf.Round((1 - player.damageResistance) * value);
			}
			else // Enemy
			{
				var enemy = target as Enemy;
				Romaji damage = (Romaji)value;
				LifeSegment life = enemy.Health.FirstOrDefault(s => s.Romaji == damage);
				if (life != null) // hit
				{
					life.damaged = true;
					life.status?.OnHit();
					if(gainFocus) Player.player.Focus += 1;
				}
			}
		}
	}
}