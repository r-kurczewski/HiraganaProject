using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class HealthBar : MonoBehaviour
	{
		public Slider health;

		public void UpdateGUI()
		{
			health.value = (float)BattlePlayer.player.Health / BattlePlayer.player.MaxHealth;
		}
	}
}