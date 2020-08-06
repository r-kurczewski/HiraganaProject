using System.Collections;
using System.Collections.Generic;
using System.Text;
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

		public void UpdateLifeString()
		{
			var sb = new StringBuilder();
			foreach (var segment in GetComponent<Enemy>().Health)
			{
				sb.Append(segment.GetFormatingString());
			}
			NameLabel.text = sb.ToString();
		}
	}
}
