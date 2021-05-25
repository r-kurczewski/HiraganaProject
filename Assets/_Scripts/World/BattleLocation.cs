using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Hiragana.Battle;
using UnityEngine.SceneManagement;
using Hiragana.Other;
using Hiragana.Battle.UI;
using BayatGames.SaveGameFree;

namespace Hiragana.World
{
	public class BattleLocation : Location
	{
		public Sprite battleBackground;

		private WorldPlayer player;
		[SerializeField] private int encounterChance = 0;
		[SerializeField][ReadOnly] private float encounterClock = 0;
		public List<EncounterChances> encounters;

		protected override string ObjectID => "location";

		private void Awake()
		{
			instance = this;
			player = FindObjectOfType<WorldPlayer>();
			StartCoroutine(BattleEncounter());
		}

		private void Update()
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
			EnemyScreen.battleBackground = battleBackground;
			SceneManager.LoadScene("Battle");
		}

		public override void Save()
		{
			SaveGame.Save(SavePath, SceneManager.GetActiveScene().buildIndex);
		}

		public override void Load()
		{
			var sceneID = SaveGame.Load<int>(SavePath);
			SceneManager.LoadScene(sceneID);
		}

		[Serializable]
		public class EncounterChances
		{
			public Encounter encounter;
			public int chances;
		}
	}
}
