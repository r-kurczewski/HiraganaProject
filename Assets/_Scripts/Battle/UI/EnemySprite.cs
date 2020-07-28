using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	[SelectionBase]
	public class EnemySprite : Selectable
	{
		public TMP_Text NameLabel { get => GetComponentInChildren<TMP_Text>(); }
		public bool keepState = false;

		static EnemyScreen list;

		protected override void Awake()
		{
			list = FindObjectOfType<EnemyScreen>();
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			list.selected = this;
		}

		public void RefreshState(bool instant)
		{
			DoStateTransition(currentSelectionState, instant);
		}

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			if (keepState) return;
			base.DoStateTransition(state, instant);
		}

		public void UpdateLife()
		{
			NameLabel.text = string.Empty;
			foreach (var segment in GetComponent<Enemy>().Health)
			{
				if (segment.damaged)
				{
					NameLabel.text += $"<color=#7774>{segment.Hiragana}</color>";
				}
				else
				{
					NameLabel.text += segment.Hiragana;
				}
			}
		}
	}
}
