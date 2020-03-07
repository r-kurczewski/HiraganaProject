using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.Selectable;

namespace Hiragana.Battle
{
	public class EnemyList : MonoBehaviour
	{
		public Enemy selected;
		public Color selectedColor;
		public Color notSelectedColor;

		[SerializeField]
		private List<Enemy> enemies = new List<Enemy>();

		void Start()
		{
			Refresh();
		}

		public void SelectEnemy(Enemy enemy, bool refresh = true)
		{
			if (refresh) Refresh();
			selected = enemy;
			enemy.Select();
		}

		public void SelectEnemy(int index)
		{
			Refresh();
			SelectEnemy(enemies[index], false);
		}

		public void DisableSelection(bool clearSelection)
		{
			Refresh();
			if (!clearSelection) selected.keepSelectTint = true;
			foreach (var en in enemies)
			{
				if (!clearSelection)
				{
					en.transition = Transition.None;
				}
				en.interactable = false;
			}
		}

		public void EnableSelection()
		{
			Refresh();
			foreach (var en in enemies)
			{
				//en.keepSelectTint = false;
				en.transition = Transition.ColorTint;
				en.interactable = true;
			}
		}

		public Enemy GetEnemy(int index)
		{
			return enemies[index];
		}

		public List<Enemy> GetEnemies()
		{
			Refresh();
			return enemies;
		}

		private void Refresh()
		{
			enemies.Clear();
			foreach (var enemy in GetComponentsInChildren<Enemy>())
			{
				enemies.Add(enemy);
			}
		}
	}
}
