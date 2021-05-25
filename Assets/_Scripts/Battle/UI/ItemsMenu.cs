using Hiragana.Other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class ItemsMenu : MenuOption
	{
		public GridManager grid;

		new void OnEnable()
		{
			// create buttons for all usable items
			foreach (var item in Inventory.inventory.battleItems)
			{
				ItemButton.Create(item, grid.transform);
			}
			int itemsInGrid = grid.gridSize.x * grid.gridSize.y;

			// fill to full grid
			while (grid.transform.childCount % itemsInGrid != 0 || grid.transform.childCount == 0)
			{
				ItemButton.Create(null, grid.transform);
			}

			firstSelection = grid.transform.GetChild(0)?.GetComponent<Selectable>();
		}

		new void OnDisable()
		{
			foreach (Transform child in grid.transform)
			{
				Destroy(child.gameObject);
			}
		}

		public override void OnEnter()
		{
			var selectedItem = EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>();
			selectedItem.Use();
		}
	}
}