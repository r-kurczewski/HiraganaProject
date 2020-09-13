using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Hiragana.Battle.Effects
{
	public class Slowness : Status, PlayerStatus
	{
		public int turns = 1;
		[SerializeField] private int value;

		public Slowness()
		{

		}

		public Slowness(Slowness org)
		{
			turns = org.turns;
			value = org.value;
		}

		public override void Apply(IBattleTarget target)
		{
			var slowness = Clone() as Slowness;
			if (!target.HaveStatus<Slowness>())
			{
				Player.player.Speed -= value;
				target.AddStatus(slowness);
			}
		}

		public override Effect Clone()
		{
			return new Slowness(this);
		}

		public override void Execute(IBattleTarget target)
		{
			if (--turns <= 0) OnRemove();
		}

		public override void Merge(Status newStatus)
		{
			var merged = newStatus as Slowness;
			turns += merged.turns;
		}

		public override void OnRemove()
		{
			base.OnRemove();
			Player.player.Speed += value;
		}
	}
}