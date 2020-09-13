using System;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	[Serializable]
	public class FocusChange : Effect
	{
		[SerializeField] private int value = 5;

		public FocusChange() { }

		public FocusChange(int value)
		{
			this.value = value;
		}

		private FocusChange(FocusChange org)
		{
			value = org.value;
		}

		public override void Apply(IBattleTarget target)
		{
			if (target is Player) Player.player.Focus += value;
			else Debug.LogWarning("Change focus applied on enemy");
		}

		public override Effect Clone()
		{
			return new FocusChange(this);
		}
	}
}