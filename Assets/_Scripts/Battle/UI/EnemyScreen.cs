using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Hiragana.Battle.Encounter;

namespace Hiragana.Battle.UI
{
	public class EnemyScreen : MonoBehaviour
	{
		public static EnemyScreen instance;

		public static Sprite battleBackground;

		public EnemySprite selected;
		public Image background;

		//[SerializeField]
		private List<EnemySprite> enemySprites = new List<EnemySprite>();

		private void Awake()
		{
			instance = this;
		}

		void Start()
		{
			UpdateList();
			if (battleBackground)
			{
				background.sprite = battleBackground;
				background.GetComponent<AspectRatioFitter>().aspectRatio = battleBackground.rect.width / battleBackground.rect.height;
			}

		}

		public void SelectEnemy(EnemySprite enemy)
		{
			selected = enemy;
			enemy.Select();
		}

		public void SelectEnemy(int index)
		{
			SelectEnemy(enemySprites[index]);
		}

		public void DisableSelection(bool keepState = false)
		{
			Debug.Log("Disable: " + keepState + ", time: " + Time.frameCount);
			foreach (var en in enemySprites)
			{
				en.keepState = keepState;
				en.interactable = false;
				en.keepState = false;
			}
		}

		public void EnableSelection()
		{
			Debug.Log("Enable time: " + Time.frameCount);
			foreach (var en in enemySprites)
			{
				en.interactable = true;
			}
		}

		public EnemySprite GetEnemy(int index)
		{
			return enemySprites[index];
		}

		public List<EnemySprite> GetEnemies()
		{
			UpdateList();
			return enemySprites;
		}

		public List<Enemy> LoadEncounter(Encounter enc)
		{
			var result = new List<Enemy>();
			foreach (EnemyPosition enPos in enc.enemies)
			{
				var en = LoadEnemy(enPos);
				result.Add(en);
			}
			return result;
		}

		Enemy LoadEnemy(EnemyPosition enPos)
		{
			var enSprite = Instantiate(Resources.Load<GameObject>("_Prefabs/Battle/Enemy"), transform).GetComponent<EnemySprite>();
			var enemy = enSprite.GetComponent<Enemy>();
			var image = enSprite.GetComponent<Image>();

			image.sprite = enPos.enemyType.sprite;
			image.GetComponent<EnemySprite>().interactable = false;
			image.GetComponent<RectTransform>().sizeDelta = new Vector2(enPos.enemyType.size, enPos.enemyType.size);
			image.GetComponent<AspectRatioFitter>().aspectRatio = image.sprite.rect.width / image.sprite.rect.height;
			enemy.name = enemy.name.Substring(0, enemy.name.Length - 7);

			enemy.type = enPos.enemyType;
			enemy.transform.localPosition = enPos.pos;

			enSprite.NameLabel.transform.localPosition += (Vector3)enPos.enemyType.lifeLabelOffset;
			return enemy;
		}

		public void RefreshSprites()
		{
			foreach (var en in enemySprites)
			{
				en.RefreshState(instant: true);
			}
		}

		private void UpdateList()
		{
			enemySprites.Clear();
			foreach (var enemy in GetComponentsInChildren<EnemySprite>())
			{
				enemySprites.Add(enemy);
			}
		}

		public void UpdateGUI()
		{
			enemySprites = enemySprites.Where(x => x != null).ToList();
			foreach (var enemy in enemySprites)
			{
				enemy.UpdateGUI();
			}
		}
	}
}
