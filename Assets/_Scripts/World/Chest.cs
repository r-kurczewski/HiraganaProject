using BayatGames.SaveGameFree;
using Hiragana.Other;
using Hiragana.World.UI;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hiragana.World
{
	public class Chest : SaveObject
	{
		[SerializeField] private SpriteRenderer spriteRenderer = default;
		[SerializeField] private Sprite closedSprite = default;
		[SerializeField] private Sprite openedSprite = default;

		public List<ItemQuantity<Item>> loot = new List<ItemQuantity<Item>>();

		[SerializeField] private bool trigger;

		[SerializeField] private bool opened = false;

		protected override string ObjectID
		{
			get
			{
				var sceneID = SceneManager.GetActiveScene().buildIndex;
				var objectID = $"C{(int)transform.position.x}x{(int)transform.position.y}";
				return sceneID + objectID;
			}
		}

		private void Start()
		{
			Load();
			if (opened) Open();
			else Close();
		}

		private void Open()
		{
			opened = true;
			spriteRenderer.sprite = openedSprite;
		}

		private void Close()
		{
			opened = false;
			spriteRenderer.sprite = closedSprite;
		}

		private void OnTriggerEnter2D()
		{
			trigger = true;
		}

		private void OnTriggerExit2D()
		{
			trigger = false;
		}

		public override void Save()
		{
			SaveGame.Save(SavePath, opened);
		}

		public override void Load()
		{
			if (SaveGame.Exists(SavePath)) opened = SaveGame.Load<bool>(SavePath);
		}

		private void Update()
		{
			#if UNITY_EDITOR
			if (trigger && Input.GetKeyDown(KeyCode.R))
			{
				Close();
			}
			#endif

			if (Input.GetButtonDown("Confirm") && trigger && !opened)
			{
				Open();
				StringBuilder sb = new StringBuilder();
				foreach (var iQuantity in loot)
				{
					sb.Append($"{iQuantity.quantity}x {iQuantity.item.name}\n");
					iQuantity.item.AddToInventory(iQuantity.quantity);
				}
				MessageLog.instance.AddMessage("Hira", "Hmm... What could be inside?");
				MessageLog.instance.AddMessage("Loot", sb.ToString());
				MessageLog.instance.ShowMessages();
			}
		}
	}
}