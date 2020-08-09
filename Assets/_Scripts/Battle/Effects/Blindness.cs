using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hiragana.Battle.Effects
{
	public class Blindness : Status, PlayerStatus
	{
		[SerializeField] private int turns;

		public override Effect Clone()
		{
			return new Blindness();
		}

		public override void Execute(IBattleTarget target)
		{
			throw new NotImplementedException();
		}

		public override void Merge(Status newStatus)
		{
			throw new NotImplementedException();
		}
	}
}