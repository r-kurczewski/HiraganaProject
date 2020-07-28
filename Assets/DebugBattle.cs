using System.Collections;
using System.Collections.Generic;
using Hiragana.Battle;
using UnityEngine;

namespace Hiragana
{
	public class DebugBattle : MonoBehaviour
	{
		public Encounter enc;

		void Start()
		{
			GetComponent<BattleScript>().LoadBattle(enc);
		}
	}
}