using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	public class ClearStatus : Effect
	{
		[SerializeField][SerializeReference][SerializeReferenceButton] private Status type;

		public ClearStatus()
		{

		}

		public ClearStatus(ClearStatus org)
		{
			type = org.type;
		}

		public override void Apply(IBattleTarget target)
		{
			var method = typeof(IBattleTarget).GetMethod("RemoveStatus");
			var generic = method.MakeGenericMethod(type.GetType());
			generic.Invoke(target, null);
		}

		public override Effect Clone()
		{
			return new ClearStatus(this);
		}
	}
}