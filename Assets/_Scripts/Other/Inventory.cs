using Hiragana.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hiragana.Other
{
	public class Inventory : MonoBehaviour
	{
		public static Inventory inventory;

		public List<ItemQuantity<BattleItem>> battleItems = new List<ItemQuantity<BattleItem>>();
		public List<KeyItem> keyItems = new List<KeyItem>();

		void Awake()
		{
			if (inventory)
			{
				Destroy(gameObject);
			}
			else
			{
				inventory = this;
				DontDestroyOnLoad(gameObject);
			}
		}

		public void Refresh()
		{
			battleItems = battleItems.Where(x => x.quantity > 0).ToList();
		}

	}
}