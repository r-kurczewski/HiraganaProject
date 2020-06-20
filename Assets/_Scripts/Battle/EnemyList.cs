using System;
using System.Collections.Generic;
using UnityEngine;

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
			UpdateList();
		}

		public void SelectEnemy(Enemy enemy)
		{
			selected = enemy;
			enemy.Select();
		}

		public void SelectEnemy(int index)
		{
			SelectEnemy(enemies[index]);
		}

		public void DisableSelection(bool keepState = false)
		{
			foreach (var en in enemies)
			{
				en.keepState = keepState;
				en.interactable = false;
			}
		}

		public void EnableSelection()
		{
			foreach (var en in enemies)
			{
				en.keepState = false;
				en.interactable = true;
			}
		}

		public Enemy GetEnemy(int index)
		{
			return enemies[index];
		}

		public List<Enemy> GetEnemies()
		{
			UpdateList();
			return enemies;
		}

		public void RefreshSprites()
		{
			foreach(var en in enemies)
			{
				en.RefreshState();
			}
		}

		private void UpdateList()
		{
			enemies.Clear();
			foreach (var enemy in GetComponentsInChildren<Enemy>())
			{
				enemies.Add(enemy);
			}
		}
	}
}
