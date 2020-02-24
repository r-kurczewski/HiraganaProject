using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

namespace Hiragana.World
{
	public class DungeonScript : MonoBehaviour
	{
		private Player player;
		private Vector2 oldPlayerPos;
		public int encounterChance;
		public List<EncounterWrapper> encounters;
		private int encounterClock = 0;

		void Start()
		{
			player = FindObjectOfType<Player>();
			oldPlayerPos = player.myRigidbody.position;
			StartCoroutine(BattleEncounter());
		}

		void Update()
		{
			if (player.IsMoving) encounterClock++;
		}

		IEnumerator BattleEncounter()
		{
			while (true)
			{
				int encounterTreshold = Random.Range(encounterChance - 100, encounterChance + 101);
				yield return new WaitUntil(() => encounterClock > encounterTreshold);

				int chanceSum = encounters.Sum(e => e.chances);

				int encounter = Random.Range(0, chanceSum + 1);

				int minChances = 0;
				int maxChances;


				for (int i = 0; i < encounters.Count; i++)
				{
					maxChances = minChances + encounters[i].chances;

					if (encounter >= minChances && encounter <= maxChances)
					{
						// Start a battle
						Debug.Log(encounters[i].encounter.name);
						encounterClock = 0;
						//yield break;
					}

					minChances = maxChances;
				}
			}
		}
		[Serializable]
		public class EncounterWrapper
		{
			public Encounter encounter;
			public int chances;
		}
	}
}
