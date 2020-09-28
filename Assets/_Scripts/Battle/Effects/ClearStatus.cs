using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	public class ClearStatus : Effect
	{
		[SerializeReference][SerializeReferenceButton] private Status clearedType;

		public ClearStatus()
		{

		}

		public ClearStatus(ClearStatus org)
		{
			clearedType = org.clearedType;
		}

		public override void Apply(IBattleTarget target)
		{
			var method = typeof(IBattleTarget).GetMethod("RemoveStatus");
			var generic = method.MakeGenericMethod(clearedType.GetType());
			generic.Invoke(target, null);
		}

		public override Effect Clone()
		{
			return new ClearStatus(this);
		}
	}
}