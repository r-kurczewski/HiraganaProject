using BayatGames.SaveGameFree;
using Hiragana.Other;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Hiragana.Puzzles
{
    public class PuzzleCreator : MonoBehaviour
    {
        public static string puzzle;
        public static bool completed;

        public string puzzleType;

        public int gridSize;
        private int puzzleSize;

        public RectTransform screenRect;
        public RectTransform puzzleRect;
        [SerializeField] private TMP_Text label = default;


        void Start()
        {
            if (puzzle != null) puzzleType = puzzle;
            label.text = puzzleType;

            if (completed)
            {
                GetComponent<GridLayoutGroup>().enabled = false;
                var image = new GameObject("Puzzle Completed").AddComponent<Image>();
                var rect = image.gameObject.GetComponent<RectTransform>();
                image.transform.SetParent(transform);
                image.sprite = Resources.Load<Sprite>("PuzzleSprites/" + puzzle);
                rect.sizeDelta = GetComponent<RectTransform>().sizeDelta;
                rect.transform.localPosition = Vector2.zero;
                return;
            }
            puzzleRect = GetComponent<RectTransform>();
            puzzleSize = (int)puzzleRect.rect.height / gridSize;
            try
            {
                Create(puzzleType);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(puzzleSize, puzzleSize);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToWorld();
            }
        }

        public void Create(string sign)
        {
            Sprite puzzleBackground = Resources.Load<Sprite>("PuzzleSprites/" + sign);
            if (!puzzleBackground) throw new NullReferenceException($"No sprite for '{sign}'.");

            int puzzleCount = gridSize * gridSize;
            var positions = GetPuzzlePosition(marginX: 100);

            for (int i = 1; i <= puzzleCount; i++)
            {
                var puzzle = Instantiate(Resources.Load<GameObject>("_Prefabs/UI/Puzzle"), transform.parent);
                var sprite = puzzle.GetComponent<Image>();

                sprite.sprite = GetPuzzleSprite(puzzleBackground, gridSize, i);
                //Vector3 pos = new Vector2((Random.value - 0.5f) * 8, (Random.value - 0.5f) * 4);

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
                foreach (var puzzle in GetComponentsInChildren<Puzzle>())
                {
                    puzzle.draggable = false;
                }
                if (puzzle != null) Debug.Log("Saved"); SaveGame.Save(World.Puzzle.GetPuzzleID(puzzleType), true);
            }
        }

        public void ReturnToWorld()
        {
            SceneManager.LoadScene(SaveGame.Load<int>("currentLocation"));
        }

        private Sprite GetPuzzleSprite(Sprite image, int gridSize, int cellNumber)
        {
            int imgCellSize = (int)(image.rect.height / gridSize);
            var indexX = ((cellNumber - 1) % gridSize);
            var indexY = gridSize - 1 - (cellNumber - 1) / gridSize;
            var puzzlePos = new Vector2(indexX * imgCellSize, indexY * imgCellSize);
            var puzzleSize = new Vector2(imgCellSize, imgCellSize);
            var pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(image.texture, new Rect(puzzlePos, puzzleSize), pivot, imgCellSize);
            sprite.name = "Puzzle " + cellNumber.ToString("d2");
            return sprite;
        }

        private IEnumerator<Vector3> GetPuzzlePosition(float marginX)
        {
            bool right = default;

            while (true)
            {
                float posX, posY;
                if (!right) posX = Random.Range(screenRect.rect.xMin + marginX, puzzleRect.rect.xMin - marginX);
                else posX = Random.Range(puzzleRect.rect.xMax + marginX, screenRect.rect.xMax - marginX);

                posY = Random.Range(puzzleRect.rect.yMin, puzzleRect.rect.yMax);

                right = !right;
                yield return new Vector3(posX, posY);
            }
        }
    }

}