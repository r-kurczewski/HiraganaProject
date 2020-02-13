using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnHover : MonoBehaviour, IPointerEnterHandler
{
	public bool enabled = true;
	public bool fromButton = true;
	public bool fromInputField = true;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!enabled) return;
		GameObject selected = EventSystem.current.currentSelectedGameObject;
		if (!fromButton && selected.GetComponent<Button>())
			return;
		else if (!fromInputField && selected.GetComponent<TMP_InputField>())
			return;
		GetComponent<Selectable>().Select();
	}
}
