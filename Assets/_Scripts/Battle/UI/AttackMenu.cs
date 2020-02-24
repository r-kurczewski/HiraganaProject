using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class AttackMenu : MenuOption
	{
		public Button submitButton;
		public TMP_InputField textField;

		private void Start()
		{
			OnVisible();
			submitButton.onClick.AddListener(() => { OnEnter(); });
		}

		protected override void OnVisible()
		{
			base.OnVisible();
			textField.text = "";
			textField.interactable = true;
			submitButton.interactable = true;
		}

		protected override void OnEnter()
		{
			TMP_InputField textField = GetComponentInChildren<TMP_InputField>();
			textField.interactable = false;
			submitButton.interactable = false;
			FindObjectOfType<BattleScript>().FocusEnemy();
			StartCoroutine(PickEnemy());
			keyListening = false;
		}

		IEnumerator PickEnemy()
		{
			yield return new WaitForEndOfFrame();
			while (true)
			{
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					Debug.Log("raycast");
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

					if (Physics.Raycast(ray, out hit))
					{
						Debug.Log(hit.transform.name);
					}
				}
				else if (Input.GetKeyDown(KeyCode.Return))
				{
					break;
				}
				Debug.Log("a");
				yield return new WaitForEndOfFrame();
			}
			//yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0));
			Enemy selected = FindObjectOfType<EnemyList>().Selected;
			Debug.Log($"Attacked {selected.name} with {textField.text}");
			textField.text = "";
			keyListening = true;
			OnEscape();
		}
	}
}
