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
			foreach (var item in BattlePlayer.player.items)
			{
				ItemButton.Create(item, grid.transform);
			}
			int itemsInGrid = grid.gridSize.x * grid.gridSize.y;

			while (grid.transform.childCount % itemsInGrid != 0) // fill to full grid
			{
				ItemButton.Create(null, grid.transform);
			}

			firstSelection = grid.transform.GetChild(0).GetComponent<Selectable>();
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