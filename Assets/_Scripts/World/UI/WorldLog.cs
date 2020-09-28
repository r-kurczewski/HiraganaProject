using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hiragana.World.UI
{
	public class WorldLog : MonoBehaviour
	{
		public static WorldLog log;

		private TMP_Text text;
		Queue<string> queue = new Queue<string>();

		private void Start()
		{
			log = this;
			text = GetComponentInChildren<TMP_Text>();
			StartCoroutine(LogUpdater());
		}

		public void ShowMessage(string message)
		{
			queue.Enqueue(message);
		}

		private IEnumerator LogUpdater()
		{
			while (true)
			{
				if (queue.Count > 0)
				{
					text.text = queue.Dequeue();
					yield return new WaitForSeconds(1);
					var a = HideLog();
					while (a.MoveNext())
					{
						yield return new WaitForEndOfFrame();
					}
					yield return new WaitForSeconds(0.5f);
				}
				else yield return new WaitForEndOfFrame();
			}
		}

		private IEnumerator HideLog()
		{
			Color newColor;
			float value = 0.01f;
			while(text.color.a >= 0)
			{
				newColor = text.color;
				newColor.a = newColor.a - value;
				text.color = newColor;
				yield return null;
			}
			text.text = "";
			newColor = text.color;
			newColor.a = 1;
			text.color = newColor;
		}
	}
}