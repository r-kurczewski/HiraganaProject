using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	[ExecuteInEditMode]
	public class FocusBar : MonoBehaviour
	{
		public Color enabledFocus;
		public Color disabledFocus;

		public void Start()
		{
			UpdateGUI();
		}

		public void UpdateGUI()
		{
			int counter = 0;
			foreach (Transform point in transform)
			{
				counter++;
				point.gameObject.SetActive(counter <= BattlePlayer.player.MaxFocus);
				point.GetComponent<Image>().color = counter <= BattlePlayer.player.Focus ? enabledFocus : disabledFocus;
			}
		}
	}
}