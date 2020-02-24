﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnHover : MonoBehaviour, IPointerEnterHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		GetComponent<Selectable>().Select();
	}
}
