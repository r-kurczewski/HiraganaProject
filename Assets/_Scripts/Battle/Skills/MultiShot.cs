using Hiragana.Battle.Effects;
using Hiragana.Battle.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.Skills
{
	[Serializable]
	public class MultiShot : Skill
	{
		public override string Name => "Multi Shot";
		public override int FocusCost => 3;
		public override SkillType Type => SkillType.Offensive;

		protected override IEnumerator Effect()
		{
			AttackMenu attackMenu = GameObject.FindObjectOfType<AttackMenu>(true);

			attackMenu.Show();
			attackMenu.keyListening = false;
			attackMenu.romajiText.interactable = true;
			attackMenu.romajiText.Select();

			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(() 
				=> Input.GetKeyDown(KeyCode.Return) 
				|| Input.GetKeyDown(KeyCode.Escape));

			if (Input.GetKeyDown(KeyCode.Return)) // ENTER
			{
				BattlePlayer.player.Focus -= FocusCost;
				foreach(var enemy in BattleScript.script.Enemies)
				{
					if (Enum.TryParse(attackMenu.romajiText.text.ToUpper(), false, out Romaji romaji))
					{
						enemy.ApplyEffect(new Damage(romaji, false));
					}
				}
				BattlePlayer.player.haveTurn = false;
			}
			else // ESCAPE
			{
				ReturnToSkillMenu();
			}
		}
	}
}