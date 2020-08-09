using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hiragana.Battle
{
	public class BattleLog : MonoBehaviour
	{
		TMP_Text text;

		void Awake()
		{
			text = GetComponentInChildren<TMP_Text>();
		}

		void OnEnable()
		{
			text.text = string.Empty;
		}

		void Start()
		{
			

		}

		public void Write(string message)
		{
			text.text = message;
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}
	}
}