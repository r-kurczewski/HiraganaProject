using BayatGames.SaveGameFree;
using Hiragana.Other;
using Hiragana.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hiragana.World
{
	public class Puzzle : SaveObject
	{
		public string puzzleType;
		[SerializeField] private bool completed;
		[SerializeField] private bool trigger;

		public static string PuzzleID(string puzzle) => $"Puzzle_{puzzle.ToLower()}";

		protected override string ObjectID => PuzzleID(puzzleType);

		private void Start()
		{
			Load();
		}

		void Update()
		{
			#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.R))
			{
				completed = false;
			}
			#endif
			if(Input.GetKeyDown(KeyCode.Return) && trigger)
			{
				LoadPuzzle();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			trigger = true;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			trigger = false;
		}

		private void LoadPuzzle()
		{
			PuzzleCreator.puzzle = puzzleType;
			PuzzleCreator.completed = completed;
			SceneManager.LoadScene("Puzzle");
		}

		public override void Save()
		{
			SaveGame.Save(SavePath, completed);
		}

		public override void Load()
		{
			if (SaveGame.Exists(SavePath)) completed = SaveGame.Load<bool>(SavePath);
		}
	}
}