using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Hiragana.Battle.BattleItem;
using static Hiragana.Battle.Attack;
using Hiragana.Other;

namespace Hiragana.Battle.UI
{
	public class ItemButton : Button
	{
		ScrollRect scroll;
		new Camera camera;
		GridManager grid;
		ItemQuantity<BattleItem> item;

		#pragma warning disable 0649
		[SerializeField] TMP_Text label;
		[SerializeField] TMP_Text quantity;
		#pragma warning restore 0649

		protected override void Awake()
		{
			base.Awake();
			grid = GetComponentInParent<GridManager>();
			scroll = GetComponentInParent<ScrollRect>();
			camera = FindObjectOfType<Camera>();
		}

		protected override void Start()
		{
			base.Start();
			UpdateGUI();
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			ScrollToItem();
		}

		public void ScrollToItem()
		{
			int rowIndex = transform.GetSiblingIndex() / grid.gridSize.x;
			int rowCount = Mathf.CeilToInt((float)scroll.content.transform.childCount / grid.gridSize.x);
			int scrollJump = grid.gridSize.y;
			int viewHeight = grid.gridSize.y;
			//rowCount += rowCount % viewHeight; // fill to full page
			float scrollVal = (float)(rowIndex - rowIndex % scrollJump) / (rowCount - viewHeight);
			scroll.verticalNormalizedPosition = 1 - scrollVal;
		}

		public void SetVisible(bool visible)
		{
			foreach (Transform child in transform)
			{
				child.gameObject.SetActive(visible);
			}
		}

		public static ItemButton Create(ItemQuantity<BattleItem> item, Transform parent)
		{
			var button = Instantiate(Resources.Load<GameObject>("_Prefabs/UI/Item"), parent).GetComponent<ItemButton>();
			button.item = item;
			return button;
		}

		public void Use()
		{
			if (item != null && item.quantity > 0)
			{
				foreach (var eff in item.item.effects)
				{
					if (eff.target is TargetType.Player)
					{
						BattlePlayer.player.ApplyEffect(eff.effect);
					}
					else throw new NotImplementedException("Wrong item target");
				}
				item.quantity--;
				Inventory.inventory.Refresh();
				BattlePlayer.player.haveTurn = false;
			}
		}

		internal void UpdateGUI()
		{
			if (item != null)
			{
				label.text = item.item.name;
				quantity.text = $"{item.quantity}x";
			}
			else SetVisible(false);
		}

	}
}