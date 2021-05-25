using UnityEngine;
using Hiragana.Other;
using System;

namespace Hiragana.World
{
	public class Door : SaveObject
	{
		[SerializeField] private Sprite openedSprite = default;

		protected override string ObjectID => throw new NotImplementedException();

		public KeyItem key;
		public bool opened;
		public bool trigger;
		public bool restoreState;

		const int underOrder = 30;
		const int aboveOrder = 60;

		private bool HaveKey => key == null || Inventory.inventory.keyItems.Contains(key);


		void Start()
		{
			if (restoreState) Load();
		}

		private void OnTriggerEnter2D()
		{
			trigger = true;
		}

		private void OnTriggerStay2D()
		{
			bool isAbove = WorldPlayer.instance.transform.localPosition.y > transform.localPosition.y;
			GetComponent<SpriteRenderer>().sortingOrder = isAbove ? aboveOrder : underOrder;
		}

		private void OnTriggerExit2D()
		{
			trigger = false;
		}

		void Update()
		{
			if (trigger && HaveKey && Input.GetKeyDown(KeyCode.Return))
			{
				Open();
			}
		}

		private void Open()
		{
			GetComponent<SpriteRenderer>().sprite = openedSprite;
			GetComponent<BoxCollider2D>().enabled = false;
		}

		public override void Save()
		{
			Debug.LogWarning("Not implemented");
		}

		public override void Load()
		{
			Debug.LogWarning("Not implemented");
		}
	}
}