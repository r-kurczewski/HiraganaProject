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
	public class Dispel : Skill
	{
		/*[SerializeField] */
		private string _name = "Dispel";
		public override string Name => _name;
		public override int FocusCost => 3;
		public override SkillType Type => SkillType.Buff;

		protected override IEnumerator Effect()
		{
			AttackMenu attackMenu = FindObjectOfType<AttackMenu>(true);
			EnemyScreen enemies = FindObjectOfType<EnemyScreen>();

			attackMenu.Show();
			attackMenu.keyListening = false;

			enemies.EnableSelection();
			if (enemies.selected == null) enemies.SelectEnemy(0);
			else enemies.SelectEnemy(enemies.selected); // reselect previous one

			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(()
				=> Input.GetKeyDown(KeyCode.Return)
				|| Input.GetKeyDown(KeyCode.Escape));

			if (Input.GetKeyDown(KeyCode.Return)) // ENTER
			{
				enemies.DisableSelection(keepState: true);
				StartCoroutine(AttackLoop());
				yield break;
			}
			else // ESCAPE
			{
				ReturnToSkillMenu();
			}
		}

		private IEnumerator AttackLoop()
		{
			AttackMenu attackMenu = FindObjectOfType<AttackMenu>(true);
			EnemyScreen enemies = FindObjectOfType<EnemyScreen>();

			attackMenu.romajiText.interactable = false; // bug workaround
			attackMenu.romajiText.interactable = true;
			attackMenu.romajiText.text = "";
			attackMenu.romajiText.Select();
			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(()
			=> Input.GetKeyDown(KeyCode.Return)
			|| Input.GetKeyDown(KeyCode.Escape));

			if (Input.GetKeyDown(KeyCode.Return)) // ENTER
			{
				var enemy = enemies.selected.GetComponent<Enemy>();
				if (Enum.TryParse(attackMenu.romajiText.text.ToUpper(), false, out Romaji romaji)) // if parsing ok hit
				{
					var life = enemy.Health.FirstOrDefault(x => !x.damaged && x.Romaji == romaji);
					life.status = null;
				}
			}
			else // ESCAPE
			{
				StartCoroutine(Effect());
				yield break;
			}

			enemies.selected = null;
			enemies.EnableSelection(); // bug workaround
			enemies.DisableSelection(false);
			Player.player.Focus -= FocusCost;
			Player.player.haveTurn = false;
		}
	}
}