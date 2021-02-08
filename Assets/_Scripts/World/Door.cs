using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hiragana.Other;
using UnityEngine.UI;
using Hiragana.Battle;

namespace Hiragana.World
{
	public class Door : SaveObject
	{
		[SerializeField] private Sprite openedSprite;

		public KeyItem key;
		public bool opened;
		public bool trigger;
		public bool restoreState;

		const int underOrder = 30;
		const int aboveOrder = 60;

		private bool HaveKey => key == null || Inventory.inventory.keyItems.Contains(key);

		void Start()
		{
			if(restoreState) Load();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			trigger = true;
		}

		private void OnTriggerStay2D()
		{
			bool isAbove = WorldPlayer.player.transform.localPosition.y > transform.localPosition.y;
			GetComponent<SpriteRenderer>().sortingOrder = isAbove ? aboveOrder : underOrder; 
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			trigger = false;
		}

		private void OnDestroy()
		{
			if(restoreState) Save();
		}

		void Update()
		{
			if (trigger && HaveKey && Input.GetKeyDown(KeyCode.Return))
			{
				GetComponent<SpriteRenderer>().sprite = openedSprite;
				GetComponent<BoxCollider2D>().enabled = false;
			}
		}

		public override void Load()
		{
			Debug.LogWarning("Not implemented.");
		}

		public override void Save()
		{
			Debug.LogWarning("Not implemented.");
		}
	}
}