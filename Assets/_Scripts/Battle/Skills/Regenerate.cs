using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle.Skills
{
	[Serializable]
	public class Regenerate : Skill
	{
		/*[SerializeField]*/ private string _name = "Regenerate";
		public override string Name => _name;
		public override int FocusCost => 3;
		public override SkillType Type => SkillType.Buff;

		protected override IEnumerator Effect()
		{
			Player.player.Focus -= FocusCost;
			Player.player.Health += 50;
			Player.player.haveTurn = false;
			yield return null;
		}
	}
}