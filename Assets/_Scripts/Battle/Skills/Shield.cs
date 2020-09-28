using Hiragana.Battle.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Skills
{
	[Serializable]
	public class Shield : Skill
	{
		public override string Name => "Shield";
		public override int FocusCost => 2;
		public override SkillType Type => SkillType.Defensive;

		protected override IEnumerator Effect()
		{
			BattlePlayer.player.Focus -= FocusCost; 
			BattlePlayer.player.ApplyEffect(new Resistance(3, 0.50f));
			BattlePlayer.player.haveTurn = false;
			yield return null;
		}
	}
}