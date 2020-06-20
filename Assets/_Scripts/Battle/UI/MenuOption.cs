using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public abstract class MenuOption : MonoBehaviour
	{
		public MenuOption parent;
		public Selectable firstSelection;
		public bool keyListening = true;

		protected void Update()
		{
			if (!keyListening) return;
			if (Input.GetButtonDown("Submit"))
			{
				OnEnter();
			}
			else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
			{
				OnEscape();
			}
		}

		public void Show()
		{
			FindObjectOfType<MenuOption>()?.gameObject.SetActive(false);
			gameObject.SetActive(true);
			firstSelection?.Select();
		}

		public virtual void OnEscape()
		{
			parent?.Show();
		}

		public virtual void OnEnter()
		{
			EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
		}
	}
}
