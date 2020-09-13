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
		/*[SerializeField]*/ private string _name = "Small Shield";
		public override string Name => _name;
		public override int FocusCost => 2;
		public override SkillType Type => SkillType.Defensive;

		protected override IEnumerator Effect()
		{
			Player.player.Focus -= FocusCost; 
			Player.player.ApplyEffect(new Resistance(3, 0.50f));
			Player.player.haveTurn = false;
			yield return null;
		}
	}
}