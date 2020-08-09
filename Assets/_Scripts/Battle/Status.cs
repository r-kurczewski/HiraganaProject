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
		public bool Keep { get; set; } = true;

		public override void Apply(IBattleTarget target)
		{
			target.AddStatus(Clone() as Status);
		}

		public abstract void Execute(IBattleTarget target);

		public abstract void Merge(Status newStatus);
	}
}