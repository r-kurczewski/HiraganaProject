using System.Collections;
using System.Collections.Generic;
using Hiragana.Battle;
using Hiragana.Battle.Skills;
using Hiragana.Battle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana
{
	public class DebugBattle : MonoBehaviour
	{
		public Encounter enc;

		void Start()
		{
			var coroutine = GetComponent<BattleScript>().LoadBattle(enc).GetEnumerator();
			StartCoroutine(coroutine);
		}

		public void TestMessage()
		{
			Debug.Log("Test message.");
		}

		public void TestAction(bool bol)
		{
			FindObjectOfType<EnemyScreen>().DisableSelection(false);
		}
	}
}