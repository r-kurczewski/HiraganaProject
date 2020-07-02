using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnHover : MonoBehaviour, IPointerEnterHandler
{
	private void Start()
	{
		return;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (GetComponent<Selectable>().interactable)
			GetComponent<Selectable>().Select();
	}
}
