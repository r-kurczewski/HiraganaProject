using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class MenuOption : MonoBehaviour
	{
		public MenuOption parent;
		public Selectable firstSelection;
		public bool keyListening = true;

		protected void OnEnable()
		{
			if (EventSystem.current.currentSelectedGameObject != firstSelection) firstSelection?.Select();
		}

		protected void OnDisable()
		{
			keyListening = true;
		}

		protected IEnumerator SelectButtton()
		{
			yield return null;
			firstSelection.Select();
		}

		protected void Update()
		{
			if (!keyListening) return;
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
			{
				OnEscape();
			}
			else if (Input.GetKeyDown(KeyCode.Return))
			{
				OnEnter();
			}
		}

		public virtual void Show()
		{
			if (!gameObject.activeSelf)
			{
				FindObjectOfType<MenuOption>()?.gameObject.SetActive(false);
				gameObject.SetActive(true);
			}
			if (firstSelection != null)
			{
				firstSelection.interactable = false; // bug workaround
				firstSelection.interactable = true; // bug workaround
				firstSelection?.Select();
			}
		}

		public virtual void OnEscape()
		{
			parent?.Show();
		}

		public virtual void OnEnter()
		{

		}
	}
}
