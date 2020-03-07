using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class KeyboardAttack : MonoBehaviour, IChooseAttackTarget
	{
		public AttackMenu menu;
		public EnemyList enemies;
		public Button testButton;

		public IEnumerator ChooseEnemy()
		{
			menu.keyListening = false;
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
			if (Input.GetKeyDown(KeyCode.Escape))
			{

				menu.OnEscape();
				menu.keyListening = true;
			}
			else
			{
				enemies.selected.keepSelectTint = true;
				StartCoroutine(menu.input.TypeHiragana());
			}
		}

		public IEnumerator TypeHiragana()
		{
			menu.textField.interactable = true;
			menu.submitButton.interactable = true;
			menu.textField.Select();
			yield return new WaitForEndOfFrame();
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape));
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				enemies.selected.keepSelectTint = false;
				enemies.EnableSelection();
				menu.textField.text = "";
				StartCoroutine(menu.input.ChooseEnemy());
			}
			else
			{
				menu.OnEscape();
				menu.keyListening = true;
				Debug.Log($"Attacked {enemies.selected.name} with {menu.textField.text}");
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
