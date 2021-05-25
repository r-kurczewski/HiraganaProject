using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;
using Hiragana.World;

namespace Hiragana.Other
{
	public class SaveManager : MonoBehaviour
	{
		public static string TempSaveDirectory => "TempSave/";

		public static string SaveDirectory(int slot) => $"Save {slot.ToString("d2")}/";

		public static void GameSave(int slot)
		{
			Debug.Log("Saving...");
			string savePath = Application.persistentDataPath + "/" + SaveDirectory(slot);
			string tempSavePath = Application.persistentDataPath + "/" + TempSaveDirectory;

			if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

			foreach (var oldFilePath in Directory.GetFiles(savePath))
			{
				File.Delete(oldFilePath);
			}

			foreach (var saveable in FindObjectsOfType<SaveObject>())
			{
				saveable.Save();
			}

			if (Directory.Exists(tempSavePath))
			{
				foreach (string filePath in Directory.GetFiles(tempSavePath))
				{
					string newPath = filePath.Replace(TempSaveDirectory, SaveDirectory(slot));
					File.Copy(filePath, newPath);
				}
			}
		}

		public static void GameLoad(int slot)
		{
			Debug.Log("Loading...");
			string savePath = Application.persistentDataPath + "/" + SaveDirectory(slot);
			string tempSavePath = Application.persistentDataPath + "/" + TempSaveDirectory;

			if (!Directory.Exists(savePath)) throw new IOException("Save not found.");

			if (!Directory.Exists(tempSavePath)) Directory.CreateDirectory(tempSavePath);

			foreach (string oldFilePath in Directory.GetFiles(tempSavePath))
			{
				File.Delete(oldFilePath);
			}

			foreach (var saveable in FindObjectsOfType<SaveObject>())
			{
				saveable.autoSave = false;
			}

			foreach (string filePath in Directory.GetFiles(savePath))
			{
				string newPath = filePath.Replace(SaveDirectory(slot), TempSaveDirectory);
				File.Copy(filePath, newPath);
			}

			Location.LoadLastLocation();
		}
	}
}