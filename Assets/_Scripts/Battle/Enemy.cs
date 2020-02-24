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

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			FindObjectOfType<EnemyList>().SelectEnemy(this);
			Debug.Log($"{name} selected");
		}
	}
}
