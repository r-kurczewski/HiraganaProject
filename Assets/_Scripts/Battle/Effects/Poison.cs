using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	[Serializable]
	public class Poison : PlayerStatus
	{
		[SerializeField] private int value = 5;
		[SerializeField] private int turns = 3;

		public Poison()
		{

		}

		public Poison(int value, int turns)
		{
			this.value = value;
			this.turns = turns;
		}

		public override bool Execute(IBattleTarget target)
		{
			throw new NotImplementedException();
		}
	}
}