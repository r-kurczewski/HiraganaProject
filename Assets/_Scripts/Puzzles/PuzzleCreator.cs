using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Hiragana.Battle.Enemy;
using Random = UnityEngine.Random;

namespace Hiragana.Puzzles
{
	public class PuzzleCreator : MonoBehaviour
	{
		public string letter;
		public int gridSize;
		private int puzzleSize;

		public RectTransform screen;
		public RectTransform puzzle;

		void Start()
		{
			puzzle = GetComponent<RectTransform>();

			puzzleSize = (int)puzzle.rect.height / gridSize;
			try
			{
				Create(letter, transform);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
			}
			GetComponent<GridLayoutGroup>().cellSize = new Vector2(puzzleSize, puzzleSize);
		}

		public void Create(string sign, Transform parent)
		{
			Sprite puzzleBackground = Resources.Load<Sprite>("PuzzleSprites/" + sign);
			if (!puzzleBackground) throw new NullReferenceException("No sprite for " + sign + ".");

			int puzzleCount = gridSize * gridSize;
			var positions = GetRandomPuzzlePosition(marginX: 100);

			for (int i = 1; i <= puzzleCount; i++)
			{
				var puzzle = Instantiate(Resources.Load<GameObject>("_Prefabs/UI/Puzzle"), transform.parent);
				var sprite = puzzle.GetComponent<Image>();

				sprite.sprite = GetPuzzleSprite(puzzleBackground, gridSize, i);
				Vector3 pos = new Vector2((Random.value - 0.5f) * 8, (Random.value - 0.5f) * 4);

				positions.MoveNext();
				puzzle.transform.localPosition = positions.Current;
				puzzle.GetComponent<RectTransform>().sizeDelta = new Vector2(puzzleSize, puzzleSize);
				puzzle.name = "Puzzle " + i.ToString("d2");
				puzzle.GetComponent<Puzzle>().puzzleNumber = i;
			}
		}

		public void CheckPuzzle()
		{
			int correct = GetComponentsInChildren<PuzzleSlot>().Count(x => x.Correct);
			if (correct == gridSize * gridSize)
			{
				// TODO
				Debug.Log("Gratz!");
			}
		}

		private Sprite GetPuzzleSprite(Sprite image, int gridSize, int cellNumber)
		{
			int imgCellSize = (int)(image.rect.height / gridSize);
			var indexX = ( (cellNumber - 1) % gridSize);
			var indexY = gridSize - 1 - (cellNumber - 1) / gridSize ;
			var puzzlePos = new Vector2(indexX * imgCellSize, indexY * imgCellSize);
			var puzzleSize = new Vector2(imgCellSize, imgCellSize);
			var pivot = new Vector2(0.5f, 0.5f);
			Sprite sprite = Sprite.Create(image.texture, new Rect(puzzlePos, puzzleSize), pivot, imgCellSize);
			sprite.name = "Puzzle " + cellNumber.ToString("d2");
			return sprite;
		}

		private IEnumerator<Vector3> GetRandomPuzzlePosition(float marginX)
		{
			bool right = default;

			while (true)
			{
				float posX, posY;
				if (!right) posX = Random.Range(screen.rect.xMin + marginX, puzzle.rect.xMin - marginX);
				else posX = Random.Range(puzzle.rect.xMax + marginX, screen.rect.xMax - marginX);

				posY = Random.Range(puzzle.rect.yMin, puzzle.rect.yMax);

				right = !right;
				yield return new Vector3(posX, posY);
			}
		}
	}

}