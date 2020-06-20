using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle
{
	[SelectionBase]
	public class Enemy : Selectable
	{
		public TMP_Text NameLabel { get => GetComponentInChildren<TMP_Text>(); }
		public bool keepState = false;

		static EnemyList list;

		protected override void Awake()
		{
			list = FindObjectOfType<EnemyList>();
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			list.selected = this;
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
		}

		public void RefreshState()
		{
			Start();
		}

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			if (keepState) return;

			if (state == SelectionState.Disabled)
			{
				targetGraphic.color = colors.disabledColor;
			}
			else if (state == SelectionState.Pressed)
			{

			}
			else if (state == SelectionState.Highlighted)
			{

			}
			else if (state == SelectionState.Selected)
			{
				targetGraphic.color = colors.selectedColor;
			}
			else if (state == SelectionState.Normal)
			{
				targetGraphic.color = colors.normalColor;
			}
		}
	}
}
