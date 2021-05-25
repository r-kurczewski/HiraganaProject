using BayatGames.SaveGameFree;
using Hiragana.Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hiragana.World
{
	public class Location : SaveObject
	{
		public static Location instance;

		protected static string saveID = "location"; 

		protected override string ObjectID => saveID;

		public string locationName;

		private void Awake()
		{
			instance = this;
		}

		public override void Save()
		{
			SaveGame.Save(SavePath, SceneManager.GetActiveScene().buildIndex);
		}

		public override void Load()
		{
			var sceneID = SaveGame.Load<int>(SavePath);
			SceneManager.LoadScene(sceneID);
		}

		public static void LoadLastLocation()
		{
			var sceneID = SaveGame.Load<int>(GetSavePath(saveID));
			SceneManager.LoadScene(sceneID);
		}
	}
}
