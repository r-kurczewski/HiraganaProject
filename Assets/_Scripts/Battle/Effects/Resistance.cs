using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	public class Resistance : Status, PlayerStatus
	{
		[SerializeField] private int turns;
		[SerializeField] private float resistValue;

		public Resistance()
		{

		}

		public Resistance(int turns, float resistValue)
		{
			this.turns = turns;
			this.resistValue = resistValue;
		}

		private Resistance(Resistance org)
		{
			turns = org.turns;
			resistValue = org.resistValue;
		}

		public override void Apply(IBattleTarget target)
		{
			Player.player.damageResistance += resistValue;
			base.Apply(target);
		}

		public override Effect Clone()
		{
			return new Resistance(this);
		}

		public override void Execute(IBattleTarget target)
		{
			if (--turns <= 0) OnRemove();
		}

		public override void Merge(Status newStatus)
		{
			var merged = newStatus as Resistance;

			resistValue = (turns * resistValue + merged.turns * merged.resistValue) / (turns + merged.turns);
			turns += merged.turns;
		}

		public override void OnRemove()
		{
			base.OnRemove();
			Player.player.damageResistance -= resistValue;
		}
	}
}