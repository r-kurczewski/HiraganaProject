using UnityEngine;

namespace Hiragana.Other
{
	public abstract class SaveObject : MonoBehaviour
	{
		protected abstract string ObjectID { get; }

		[HideInInspector] public bool autoSave = true;

		protected string SavePath => SaveManager.TempSaveDirectory + ObjectID;

		public static string GetSavePath(string objectID) => SaveManager.TempSaveDirectory + objectID;

		protected void OnDestroy()
		{
			if (autoSave)
			{
				Save();
				//Debug.Log($"Autosave... ({name})");
			}
		}

		public abstract void Save();

		public abstract void Load();
	}
}