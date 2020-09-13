using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hiragana.Battle
{
	public class BattleLog : MonoBehaviour
	{
		public static BattleLog log;
		public TMP_Text text;

		private bool visible = false;

		void Awake()
		{
			log = this;
		}

		void OnEnable()
		{
			text.text = string.Empty;
		}

		public void Write(string message)
		{
			if (!visible) SetVisibility(true);
			text.text = message;
		}

		public void SetVisibility(bool visible)
		{
			foreach(Transform child in gameObject.transform)
			{
				child.gameObject.SetActive(visible);
				this.visible = visible;
			}
		}
	}
}