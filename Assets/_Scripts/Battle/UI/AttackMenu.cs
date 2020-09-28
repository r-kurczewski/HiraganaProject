using Hiragana.Battle.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.UI
{
	public class AttackMenu : MenuOption
	{
		public TMP_InputField romajiText;
		public EnemyScreen enemies;

		new private void OnDisable()
		{
			base.OnDisable();
			romajiText.interactable = false;
			romajiText.text = "";
		}

		public void StartAttack()
		{
			Show();
			StartCoroutine(ChooseEnemy());
		}

		public IEnumerator ChooseEnemy()
		{
			keyListening = false;
			enemies.EnableSelection();
			if (enemies.selected == null)
				enemies.SelectEnemy(0);
			else
				enemies.SelectEnemy(enemies.selected);
			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(()
				=> Input.GetKeyDown(KeyCode.Return)
				|| Input.GetKeyDown(KeyCode.Escape)
				);
			if (Input.GetKeyDown(KeyCode.Return))
			{
				enemies.DisableSelection(keepState: true);
				StartCoroutine(ChooseHiragana());
			}
			else
			{
				enemies.selected = null;
				enemies.DisableSelection(false);
				OnEscape();
			}
		}

		public IEnumerator ChooseHiragana()
		{
			romajiText.interactable = true;
			romajiText.Select();
			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(()
			=> Input.GetKeyDown(KeyCode.Return)
			|| Input.GetKeyDown(KeyCode.Escape));

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				enemies.EnableSelection();

				romajiText.text = "";
				StartCoroutine(ChooseEnemy());
			}
			else
			{
				Debug.Log($"Attacked {enemies.selected.name} with {romajiText.text}");
				var enemy = enemies.selected.GetComponent<Enemy>();
				if (Enum.TryParse(romajiText.text.ToUpper(), false, out Romaji romaji)) // if parsing ok hit
				{
					enemy.ApplyEffect(new Damage(romaji));
				}
				enemies.EnableSelection(); // bug workaround
				enemies.DisableSelection();
				BattlePlayer.player.haveTurn = false;
			}
		}
	}
}
