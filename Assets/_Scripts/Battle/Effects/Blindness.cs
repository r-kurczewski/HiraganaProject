using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hiragana.Battle.Effects
{
	public class Blindness : Status, PlayerStatus
	{
		[SerializeField] private int turns;

		public Blindness()
		{

		}

		public Blindness(Blindness org)
		{
			turns = org.turns;
		}

		public override void Apply(IBattleTarget target)
		{
			base.Apply(target);
		}

		public override Effect Clone()
		{
			return new Blindness(this);
		}

		public override void Execute(IBattleTarget target)
		{
			if (--turns <= 0) OnRemove();
		}

		public override void Merge(Status newStatus)
		{
			var merged = newStatus as Blindness;
			turns += merged.turns;
		}

		public static string GetFormatingString(string str)
		{
			return $"<color=#fff>? </color>";
		}
	}
}