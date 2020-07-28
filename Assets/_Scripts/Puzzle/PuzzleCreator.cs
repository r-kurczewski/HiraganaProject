using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

namespace Hiragana.Puzzle
{
	public class PuzzleCreator : MonoBehaviour
	{
		public string letter;
		public int size;

		void Start()
		{
			try
			{
				Create(letter, size, transform);
			}
			catch (NullReferenceException ex)
			{
				Debug.LogError(ex.Message);
			}
		}

		public void Create(string sign, int gridSize, Transform parent)
		{
			Sprite puzzleBackground = Resources.Load<Sprite>("PuzzleSprites/" + sign);
			if (!puzzleBackground) throw new NullReferenceException("No sprite for " + sign + ".");

			for (int i = 1; i <= gridSize * gridSize; i++)
			{
				var puzzle = Instantiate(Resources.Load<GameObject>("_Prefabs/UI/Puzzle"), transform);
				puzzle.GetComponent<SpriteRenderer>().sprite = GetPuzzleSprite(puzzleBackground, gridSize, i);
				Vector3 pos = new Vector2((Random.value - 0.5f) * 8, (Random.value - 0.5f) * 4);
				puzzle.transform.position += pos;
				puzzle.name = "Puzzle " + i.ToString("d2");
			}
		}

		private Sprite GetPuzzleSprite(Sprite image, int grid, int cellNumber)
		{
			Sprite sprite;
			int cellSize = (int)(image.rect.height / grid);
			var pos = new Vector2((cellNumber - 1) % grid * cellSize, (Mathf.CeilToInt((float)cellNumber / grid) - 1) * cellSize);
			var size = new Vector2(cellSize, cellSize);
			var pivot = new Vector2(0.5f, 0.5f);
			sprite = Sprite.Create(image.texture, new Rect(pos, size), pivot, cellSize);
			return sprite;
		}
	}

}