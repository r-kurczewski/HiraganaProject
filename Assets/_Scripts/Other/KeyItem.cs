using System.Collections.Generic;
using UnityEngine;
using TargetedEffect = Hiragana.Battle.Attack.TargetedEffect;

namespace Hiragana.Other
{
	[CreateAssetMenu(fileName = "KeyItem", menuName = "Other/KeyItem", order = 1)]
	public class KeyItem : Item
	{
		public override void AddToInventory(uint quantity)
		{
			var keyItems = Inventory.inventory.keyItems;
			if(!keyItems.Contains(this)) keyItems.Add(this);
		}
	}
}
