using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.World.UI
{
	public class GameMenu : MonoBehaviour
	{
		public static GameMenu instance;

		[SerializeField] private TMP_Text locationName;

		private bool opened = false;

		void Start()
		{
			instance = this;
		}

		void Update()
		{
			if (Input.GetButtonDown("Cancel"))
			{
				if (opened) CloseMenu();
				else OpenMenu();
			}
		}

		public void OpenMenu()
		{
			WorldPlayer.instance.blockMove = true;
			GetComponent<Image>().enabled = true;
			foreach (Transform item in transform)
			{
				item.gameObject.SetActive(true);
			}
			GetComponentInChildren<Button>().Select();
			opened = true;

			RefreshMenu();
		}

		public void CloseMenu()
		{
			WorldPlayer.instance.blockMove = false;
			GetComponent<Image>().enabled = false;
			foreach (Transform item in transform)
			{
				item.gameObject.SetActive(false);
			}
			opened = false;
		}

		public void RefreshMenu()
		{
			locationName.text = Location.instance.locationName;
		}
	}
}