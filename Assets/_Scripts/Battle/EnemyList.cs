using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle
{
	public class EnemyList : MonoBehaviour
	{
		[SerializeField]
		private List<Enemy> enemies = new List<Enemy>();

		[SerializeField]
		public Enemy Selected { get; private set; }

		void Start()
		{
			Refresh();
		}

		public void SelectEnemy(Enemy enemy)
		{
			foreach (var en in enemies)
			{
				if (en != enemy)
				{
					en.Sprite.color = new Color(1, 1, 1, 0.5f);
				}
				else
				{
					en.Sprite.color = Color.white;
				}
			}
			Selected = enemy;
		}

		public void SelectEnemy(int index)
		{
			SelectEnemy(enemies[index]);
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
