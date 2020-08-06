using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.Effects
{
	public class Thorns : HiraganaStatus
	{
		[SerializeField] private int damage;

		public Thorns()
		{

		}

		public Thorns(LifeSegment life, int damage) : base(life)
		{
			this.damage = damage;
		}

		public override bool Execute(IBattleTarget target)
		{
			throw new System.NotImplementedException();
		}

		public override string GetStatusFormating(string str)
		{
			return $"<color=#0bb36a><b>{str}</b></color>";
		}
	}
}