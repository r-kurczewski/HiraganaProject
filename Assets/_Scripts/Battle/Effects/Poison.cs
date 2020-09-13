using System;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	[Serializable]
	public class Poison : Status, PlayerStatus
	{
		[SerializeField] private int poison = 5;

		public Poison()
		{

		}

		private Poison(Poison org)
		{
			poison = org.poison;
		}

		public override Effect Clone()
		{
			return new Poison(this);
		}

		public override void Execute(IBattleTarget target)
		{
			Player.player.ApplyEffect(new DirectDamage(poison));
			if (--poison <= 0) OnRemove();

		}

		public override void Merge(Status newStatus)
		{
			var merged = newStatus as Poison;
			poison += merged.poison;
		}
	}
}