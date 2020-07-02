using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class KeyboardAttack : MonoBehaviour, IChooseAttackTarget
	{
		public AttackMenu menu;
		public EnemyScreen enemies;
		public Button testButton;

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
				enemies.RefreshSprites();
				menu.textField.text = "";
				menu.OnEscape();
				menu.keyListening = true;
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
