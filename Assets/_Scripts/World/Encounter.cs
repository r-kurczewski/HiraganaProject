using Hiragana.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Hiragana.World
{
	[InitializeOnLoad]
	[CreateAssetMenu(fileName = "Encounter", menuName = "Battle/Encounter", order = 1)]
	[Serializable]
	public class Encounter : ScriptableObject
	{
		public Enemy[] enemies;

		private void OnEnable()
		{
			try
			{
				Rename();
				//Debug.Log(name);
			}
			catch(Exception)
			{
				Debug.LogError("Couldn't reload Encounter name");
			}
		}

		private void OnAwake()
		{
			try
			{
				Rename();
				//Debug.Log(name);
			}
			catch (Exception)
			{
				Debug.LogError($"Couldn't reload {name} name");
			}
		}

		public void Rename()
		{
			var monsters = new Dictionary<Enemy, int>();
			foreach (var enemy in enemies)
			{
				if (monsters.ContainsKey(enemy))
				{
					monsters[enemy]++;
				}
				else
				{
					monsters.Add(enemy, 1);
				}
			}
			var names = new List<string>();
			foreach (var monster in monsters)
			{
				names.Add($"{monster.Value}x{monster.Key.name.Substring(0, 4)}");
			}
			UpdateName(string.Join(" ", names));
		}

		public void UpdateName(string newName)
		{
			string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
			AssetDatabase.RenameAsset(assetPath, newName);
			AssetDatabase.SaveAssets();
		}
	}
}
