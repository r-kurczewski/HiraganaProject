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

		private void Start()
		{
			OnVisible();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
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
			FindObjectOfType<MenuOption>()?.Hide();
			gameObject.SetActive(true);
			OnVisible();
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}

		protected virtual void OnEscape()
		{
			parent?.Show();
		}

		protected virtual void OnEnter()
		{
			EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
		}

		private void OnRightClick()
		{
			throw new NotImplementedException();
		}

		protected virtual void OnVisible()
		{
			firstSelection?.Select();
		}


	}
}
