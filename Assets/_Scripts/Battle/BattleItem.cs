using Hiragana.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TargetedEffect = Hiragana.Battle.Attack.TargetedEffect;

namespace Hiragana.Battle
{
	[CreateAssetMenu(fileName = "BattleItem", menuName = "Battle/BattleItem", order = 1)]
	public class BattleItem : Item
	{
		public List<TargetedEffect> effects = new List<TargetedEffect>();

		public override void AddToInventory(uint quantity)
		{
			ItemQuantity<BattleItem> current;
			var battleItems = Inventory.inventory.battleItems;
			current = battleItems.FirstOrDefault(x => x.item == this);
			if (current is null)
			{
				battleItems.Add(new ItemQuantity<BattleItem>(this, quantity));
			}
			else
			{
				current.quantity += quantity;
			}
		}
	}
}
