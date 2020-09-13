using System;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	[Serializable]
	public class Regeneration : Status, PlayerStatus
	{
		[SerializeField] private int regeneration = 5;

		public Regeneration()
		{

		}

		private Regeneration(Regeneration org)
		{
			regeneration = org.regeneration;
		}

		public override Effect Clone()
		{
			return new Regeneration(this);
		}

		public override void Execute(IBattleTarget target)
		{
			Player.player.ApplyEffect(new Heal(regeneration));
			if (--regeneration <= 0) OnRemove();

		}

		public override void Merge(Status newStatus)
		{
			var merged = newStatus as Regeneration;
			regeneration += merged.regeneration;
		}
	}
}