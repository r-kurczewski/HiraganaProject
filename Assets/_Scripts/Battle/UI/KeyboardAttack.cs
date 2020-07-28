using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.UI
{
	public class KeyboardAttack : MonoBehaviour, IAttackInput
	{
		public AttackMenu menu;
		public EnemyScreen enemies;
		public BattleLog log;

		public IEnumerator ChooseEnemy()
		{
			menu.keyListening = false;
			enemies.EnableSelection();
			menu.textField.interactable = true;
			if (enemies.selected == null)
				enemies.SelectEnemy(0);
			else
				enemies.SelectEnemy(enemies.selected);
			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(()
				=> Input.GetKeyDown(KeyCode.Return)
				|| Input.GetKeyDown(KeyCode.Escape)
				);
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				enemies.selected = null;
				enemies.DisableSelection(false);
				menu.OnEscape();
				menu.keyListening = true;
			}
			else
			{
				enemies.DisableSelection(keepState: true);
				StartCoroutine(menu.input.TypeHiragana());
			}
		}

		public IEnumerator TypeHiragana()
		{ 
			menu.textField.Select();
			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape));
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				enemies.EnableSelection();

				menu.textField.text = "";
				StartCoroutine(menu.input.ChooseEnemy());
			}
			else
			{
				Debug.Log($"Attacked {enemies.selected.name} with {menu.textField.text}");
				Enum.TryParse(menu.textField.text.ToUpper(), out Romaji romaji);
				enemies.selected.GetComponent<IBattleTarget>().ApplyDamage((int)romaji);
				enemies.RefreshSprites();
				menu.textField.text = "";
				menu.OnEscape();
				FindObjectOfType<MenuOption>().Hide();
				log.gameObject.SetActive(true);
				menu.keyListening = true;
				FindObjectOfType<PlayerData>().haveTurn = false;
			}
		}

		public IEnumerator ConfirmAttack()
		{
			menu.keyListening = false;
			yield return new WaitForEndOfFrame();
			menu.keyListening = true;
		}
	}
}
