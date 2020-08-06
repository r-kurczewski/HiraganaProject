using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Hiragana.Battle
{
	[Serializable]
	public abstract class Status : Effect
	{
		public override bool Apply(IBattleTarget target)
		{
			return target.AddStatus(this);
		}

		public abstract bool Execute(IBattleTarget target);
	}
}