using Hiragana.Battle.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	[Serializable]
	public class Stun : Status, PlayerStatus, EnemyStatus
	{
		[SerializeField] private int turns = 1;

		public Stun()
		{

		}

		private Stun(Stun org)
		{
			turns = org.turns;
		}

		public override Effect Clone()
		{
			return new Stun(this);
		}

		public override void Execute(IBattleTarget target)
		{
			turns--;
			target.SkipTurn = true;
			if (turns <= 0) Keep = false;
		}

		public override void Merge(Status newStatus)
		{
			var merged = newStatus as Stun;
			turns += merged.turns;
		}
	}
}
