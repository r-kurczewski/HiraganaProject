using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class PlayerPanel : MonoBehaviour
	{
		#pragma warning disable 0649
		[SerializeField] private HealthBar healthBar;
		[SerializeField] private FocusBar focusBar;
		[SerializeField] private StatusList statusList;
		#pragma warning restore 0649

		public void UpdateGUI()
		{
			healthBar.UpdateGUI();
			statusList.UpdateGUI();
			focusBar.UpdateGUI();
		}
	}
}