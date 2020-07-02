using System.Collections.Generic;
using UnityEngine;
using static Hiragana.Battle.Encounter;

namespace Hiragana.Battle.UI
{
	public class EnemyScreen : MonoBehaviour
	{
		public static EnemyScreen context;
		public EnemySprite selected;

		[SerializeField]
		private List<EnemySprite> enemies = new List<EnemySprite>();

		private void Awake()
		{
			context = this;
		}

		void Start()
		{
			UpdateList();
		}

		public void SelectEnemy(EnemySprite enemy)
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
				en.keepState = false;
			}
		}

		public void EnableSelection()
		{
			foreach (var en in enemies)
			{
				en.interactable = true;
			}
		}

		public EnemySprite GetEnemy(int index)
		{
			return enemies[index];
		}

		public List<EnemySprite> GetEnemies()
		{
			UpdateList();
			return enemies;
		}

		public List<Enemy> LoadEncounter(Encounter enc)
		{
			List<Enemy> result = new List<Enemy>();
			foreach (EnemyPosition enPos in enc.enemies)
			{
				EnemySprite sprite = Instantiate(enPos.enemy.spritePrefab, transform).GetComponent<EnemySprite>();
				sprite.interactable = false;
				Enemy enemy = sprite.gameObject.AddComponent<Enemy>();
				result.Add(enemy);
				enemy.type = enPos.enemy;
				sprite.transform.localPosition = enPos.pos;
			}
			return result;
		}

		void AddEnemy(EnemyType enemy)
		{
			var en = Instantiate(enemy.spritePrefab, transform).GetComponent<EnemySprite>();
			en.interactable = false;
		}

		public void RefreshSprites()
		{
			foreach(var en in enemies)
			{
				en.RefreshState(instant: true);
			}
		}

		private void UpdateList()
		{
			enemies.Clear();
			foreach (var enemy in GetComponentsInChildren<EnemySprite>())
			{
				enemies.Add(enemy);
			}
		}
	}
}
