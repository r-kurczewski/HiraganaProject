using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Hiragana.Battle;
using UnityEngine.SceneManagement;

namespace Hiragana.World
{
	public class Location : MonoBehaviour
	{
		public static Location location;

		private WorldPlayer player;
		[SerializeField] private int encounterChance = 0;
		[SerializeField] private float encounterClock = 0;
		public List<EncounterChances> encounters;

		void Awake()
		{
			location = this;
			player = FindObjectOfType<WorldPlayer>();
			StartCoroutine(BattleEncounter());
		}

		void Update()
		{
			if (player.IsMoving)
			{
				encounterClock += Time.deltaTime;
			}
		}

		IEnumerator BattleEncounter()
		{
			while (true)
			{
				var encounterTreshold = encounterChance * Random.Range(0.80f, 1.20f);
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
						LoadBattle(encounters[i].encounter);
						encounterClock = 0;
						yield break;
					}

					minChances = maxChances;
				}
			}
		}

		private void LoadBattle(Encounter encounter)
		{
			BattleScript.currentEncounter = encounter;
			Debug.Log(player.transform.position);
			WorldPlayer.player.Deactivate();
			WorldPlayer.locationId = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene("Battle");
		}

	}

	[Serializable]
	public class EncounterChances
	{
		public Encounter encounter;
		public int chances;
	}
}
