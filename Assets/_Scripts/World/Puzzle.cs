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

		private string PuzzleID => GetPuzzleID(puzzleType);

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
				SaveGame.Save(PuzzleID, completed);
			}
			#endif
			if(Input.GetKeyDown(KeyCode.Return) && trigger)
			{
				LoadPuzzle();
			}
		}
		private void OnDestroy()
		{
			Save();
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

		public static string GetPuzzleID(string puzzle)
		{
			return $"Puzzle_{puzzle.ToLower()}";
		}

		public override void Save()
		{
			return;
		}

		public override void Load()
		{
			if(SaveGame.Exists(PuzzleID)) completed = SaveGame.Load<bool>(PuzzleID);
		}
	}
}