using System.Collections;
using System.Collections.Generic;
using Hiragana.Battle;
using UnityEngine;

namespace Hiragana
{
	public class DebugBattle : MonoBehaviour
	{
		public Encounter enc;
		public BattleScript battleScript;

		void Start()
		{
			battleScript.LoadBattle(enc);
		}
	}
}