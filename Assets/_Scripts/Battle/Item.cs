using System;
using System.Collections.Generic;
using UnityEngine;
using TargetedEffect = Hiragana.Battle.Attack.TargetedEffect;

namespace Hiragana.Battle
{
	[ExecuteInEditMode]
	[CreateAssetMenu(fileName = "Item", menuName = "Battle/Item", order = 1)]
	public class Item : ScriptableObject
	{
		public string displayName;
		public List<TargetedEffect> effects = new List<TargetedEffect>();

		public void UpdateDisplayName()
		{
			if (displayName == "" && name != "")
			{
				Debug.Log($"Set visible name to '{name}'", this);
				displayName = name;
			}
		}

		private void OnEnable()
		{
			UpdateDisplayName();
		}

		public override string ToString()
		{
			return name;
		}

		[Serializable]
		public class ItemQuantity
		{
			public Item item;
			public int quantity;

			public ItemQuantity() { }

			public ItemQuantity(Item item, int quantity)
			{
				this.item = item;
				this.quantity = quantity;
			}
		}
	}
}
