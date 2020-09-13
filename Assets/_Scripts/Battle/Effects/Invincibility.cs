using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	public class Invincibility : HiraganaStatus
	{
		[SerializeField] private int turns = 1;

		public Invincibility()
		{

		}

		private Invincibility(Invincibility org) : base(org)
		{
			turns = org.turns;
		}

		public override Effect Clone()
		{
			return new Invincibility(this);
		}

		public override void Execute(IBattleTarget target)
		{
			if (--turns <= 0) OnRemove();
		}

		public override string GetStatusFormating(string str)
		{
			return $"<color=#757575>{str}</color>";
		}

		public override void OnHit()
		{
			life.damaged = false;
		}
	}
}