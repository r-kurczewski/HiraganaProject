using BayatGames.SaveGameFree;
using Hiragana.Other;
using Hiragana.World.UI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hiragana.World
{
	public class Warp : MonoBehaviour
	{
		public bool confirmWarp;
		public bool trigger;
		public Vector2 pos;
		public Vector2 direction;
		public int sceneID;

		private void Update()
		{
			if (trigger && Input.GetKeyDown(KeyCode.Return))
			{
				UseWarp();
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			trigger = false;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (confirmWarp)
			{
				trigger = true;
			}
			else
			{
				UseWarp();
			}

		}

		private void UseWarp()
		{
			WorldPlayer.instance.autoSave = false;
			WorldPlayer.instance.SaveStartPosition(pos, direction.x, direction.y);
			SceneManager.LoadScene(sceneID);
		}
	}
}