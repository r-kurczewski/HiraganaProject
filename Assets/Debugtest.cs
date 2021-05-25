using BayatGames.SaveGameFree;
using Hiragana.Battle.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana
{
	public class Debugtest : MonoBehaviour
	{
		public EnemyScreen screen;
		void Start()
		{
			
		}

		void Update()
		{

		}

		public void TurnOff(bool keepState)
        {
			screen.DisableSelection(keepState);
        }
	}
}