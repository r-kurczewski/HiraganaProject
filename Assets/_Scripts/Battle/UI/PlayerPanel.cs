using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class PlayerPanel : MonoBehaviour
	{
		public Slider lifeBar;
		public Slider focusBar;
		public Player player;

		public void UpdateGUI()
		{
			lifeBar.value = (float)player.Health / player.MaxHealth;
		}
	}
}