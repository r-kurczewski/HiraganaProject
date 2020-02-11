using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class AttackMenu : MenuOption
	{
		public Button submitButton;

		private void Start()
		{
			submitButton.onClick.AddListener(() => { OnEnter(); });
			OnVisible();
		}

		protected override void OnEnter()
		{
			TMP_InputField textField = GetComponentInChildren<TMP_InputField>();
			Debug.Log($"Attack with {textField.text}!");
			textField.text = "";
			OnEscape();
		}
	}
}
