using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Skills
{
	[Serializable]
	public class Regenerate : Skill
	{
		public override string Name => "Regenrate";
		public override int FocusCost => 3;
		public override SkillType Type => SkillType.Buff;

		protected override IEnumerator Effect()
		{
			BattlePlayer.player.Focus -= FocusCost;
			BattlePlayer.player.Health += 50;
			BattlePlayer.player.haveTurn = false;
			yield return null;
		}
	}
}