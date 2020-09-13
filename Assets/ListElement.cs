using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana
{
	public class ListElement : Button
	{
		RectTransform window;
		RectTransform rect;
		int counter = 0;
		int viewHeight = 4;
		int scrollJump = 2;

		new void Start()
		{
			window = GetComponentInParent<ScrollRect>().GetComponent<RectTransform>();
			rect = GetComponent<RectTransform>();
		}

		public void ShowInTabControl()
		{
			var scroll = window.GetComponent<ScrollRect>();

			int index = transform.GetSiblingIndex();
			float scrollVal = (float)(index - index % scrollJump) / (scroll.content.transform.childCount - viewHeight);
			scroll.verticalNormalizedPosition = 1 - scrollVal;
			Debug.Log(1 - scrollVal);
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			ShowInTabControl();
		}
	}
}