using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hiragana.Battle.Effects
{
	public class Slowness : Status, PlayerStatus
	{
		public Slowness()
		{

		}

		public Slowness(Slowness org)
		{

		}

		public override Effect Clone()
		{
			return new Slowness(this);
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