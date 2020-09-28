using BayatGames.SaveGameFree;
using Hiragana.World.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Hiragana.Battle.Item;

namespace Hiragana.World
{
	public class Chest : MonoBehaviour
	{
		static int chestID;
		[SerializeField] private Sprite openSprite;
		[SerializeField] private Sprite closeSprite;
		public List<ItemQuantity> loot = new List<ItemQuantity>();

		private bool trigger;

		private bool Opened
		{
			get
			{
				if (SaveGame.Exists(ChestID))
				{
					return SaveGame.Load<bool>(ChestID);
				}
				else return false;
			}
			set
			{
				SaveGame.Save(ChestID, value);
			}
		}

		public string ChestID
		{
			get
			{
				var sceneID = SceneManager.GetActiveScene().buildIndex;
				var uniqueID = $"{sceneID}_{(int)transform.position.x}x{(int)transform.position.y}";
				return uniqueID;
			}
		}

		private void Start()
		{
			if (Opened) GetComponent<SpriteRenderer>().sprite = openSprite;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			trigger = true;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			trigger = false;
		}

		private void Update()
		{
			#if UNITY_EDITOR
			if (trigger && Input.GetKeyDown(KeyCode.R))
			{
				Opened = false;
				GetComponent<SpriteRenderer>().sprite = closeSprite;
			}
			#endif
			if (Input.GetKeyDown(KeyCode.Return) && trigger && !Opened)
			{
				Opened = true;
				StringBuilder sb = new StringBuilder();
				foreach (var item in loot)
				{
					sb.Append($"{item.quantity}x {item.item.name}\n");
					Battle.BattlePlayer.player.AddItem(item);
				}
				WorldLog.log.ShowMessage(sb.ToString());

				GetComponent<SpriteRenderer>().sprite = openSprite;
			}
		}
	}
}