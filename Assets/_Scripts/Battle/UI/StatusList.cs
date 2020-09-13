using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sprite = UnityEngine.Sprite;

namespace Hiragana.Battle.UI
{
	public class StatusList : MonoBehaviour
	{
		void Start()
		{
			Clear();
		}

		public void UpdateGUI()
		{
			Clear();
			foreach(var status in Player.player.statuses)
			{
				GameObject prefab = Instantiate(Resources.Load<GameObject>("_Prefabs/UI/Status"), transform);
				prefab.name = status.GetType().Name;
				Image image = prefab.GetComponent<Image>();
				var sprite = Resources.Load<Sprite>($"Statuses/{status.GetType().Name}");
				if (sprite != null) image.sprite = sprite;
				else Debug.LogError($"No sprite for {status.GetType().Name} loaded.");
			}
		}

		private void Clear()
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}
		}
	}
}