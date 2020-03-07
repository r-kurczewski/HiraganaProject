using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle
{
	[SelectionBase]
	public class Enemy : Selectable, IPointerDownHandler
	{
		public Image Sprite { get => GetComponentInChildren<Image>(); }
		public TMP_Text NameLabel { get => GetComponentInChildren<TMP_Text>(); }

		static EnemyList list;
		public bool keepSelectTint = false;

		protected override void Awake()
		{
			list = FindObjectOfType<EnemyList>();
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			FindObjectOfType<EnemyList>().SelectEnemy(this);
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			if (!keepSelectTint)
			{
				base.OnDeselect(eventData);
			}
		}

		public void SetAsDefault()
		{
			DoStateTransition(SelectionState.Normal, true);
		}

		public void SetAsSelected()
		{
			DoStateTransition(SelectionState.Selected, true);
		}

	}
}
