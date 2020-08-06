using Hiragana.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hiragana.Battle
{
	[Serializable]
	public abstract class Effect
	{
		[ReadOnly] public string type = "ERROR";

		public Effect()
		{
			type = GetType().Name;
		}

		public abstract bool Apply(IBattleTarget target);
	} 
}
