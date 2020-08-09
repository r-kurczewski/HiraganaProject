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
			poison--;
			BattleScript.script.Player.ApplyEffect(new Damage(poison));
			if (poison <= 0) Keep = false;

		}

		public override void Merge(Status newStatus)
		{
			var merged = newStatus as Poison;
			poison += merged.poison;
		}
	}
}