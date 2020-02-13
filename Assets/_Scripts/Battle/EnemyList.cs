using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Battle
{
	public class EnemyList : MonoBehaviour
	{
		[SerializeField]
		private List<Enemy> enemies = new List<Enemy>();
		Enemy selected;

		void Start()
		{
			Refresh();
		}

		public Enemy getEnemy(int index) 
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
