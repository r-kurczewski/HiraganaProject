using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	public class Thorns : HiraganaStatus
	{
		[SerializeField] private int thornsDamage = 20;

		public Thorns()
		{

		}

		public Thorns(Thorns org) : base(org)
		{
			thornsDamage = org.thornsDamage;
		}

		public override Effect Clone()
		{
			return new Thorns(this);
		}

		public override void Execute(IBattleTarget target)
		{
			return;
		}

		public override string GetStatusFormating(string str)
		{
			return $"<color=#0bb36a><b>{str}</b></color>";
		}

		public override void OnHit()
		{
			BattleScript.script.Player.ApplyEffect(new Damage(thornsDamage));
			
		}
	}
}