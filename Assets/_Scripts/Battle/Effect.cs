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
		[ReadOnly] [HideInInspector] public string type;

		public Effect()
		{
			type = GetType().Name;
		}

		public abstract Effect Clone();

		public abstract void Apply(IBattleTarget target);
	} 
}
