using Hiragana.Battle.Effects;
using Hiragana.Battle.UI;
using System;
using System.Collections;
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
			EnemyScreen enemies = GameObject.FindObjectOfType<EnemyScreen>();

			attackMenu.Show();
			attackMenu.keyListening = false;
			attackMenu.romajiText.interactable = true;
			attackMenu.romajiText.Select();

			yield return new WaitUntil(()
				=> Input.GetKeyUp(KeyCode.Return)
				|| Input.GetKeyUp(KeyCode.Escape)
			);
			yield return new WaitUntil(() 
				=> Input.GetKeyDown(KeyCode.Return) 
				|| Input.GetKeyDown(KeyCode.Escape)
			);

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
				enemies.DisableSelection(keepState: false);
				ReturnToSkillMenu();
			}
		}
	}
}