using Hiragana.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using Hiragana.Battle.UI;
using UnityEditor;

namespace Hiragana.Battle
{
	[CreateAssetMenu(fileName = "Encounter", menuName = "Battle/Encounter", order = 1)]
	[Serializable]
	public class Encounter : ScriptableObject
	{
		public EnemyPosition[] enemies;

		private void OnAwake()
		{
			#if UNITY_EDITOR
			try
			{
				Rename();
			}
			catch (Exception)
			{
				Debug.LogError($"Couldn't reload {name} name");
			}
			#endif
		}

		[Serializable]
		public class EnemyPosition
		{
			public EnemyType enemy;
			public Vector2 pos;
		}

		#region editor
#if UNITY_EDITOR
		private void Rename()
		{
			var monsters = new Dictionary<EnemyType, int>();
			foreach (EnemyPosition enPos in enemies)
			{
				if (monsters.ContainsKey(enPos.enemy))
				{
					monsters[enPos.enemy]++;
				}
				else
				{
					monsters.Add(enPos.enemy, 1);
				}
			}
			var names = new List<string>();
			foreach (var monster in monsters)
			{
				names.Add($"{monster.Value}x{monster.Key.name.Substring(0, 4)}");
			}
			UpdateName(string.Join(" ", names));
		}

		private void UpdateName(string newName)
		{
			string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
			AssetDatabase.RenameAsset(assetPath, newName);
			AssetDatabase.SaveAssets();
		}
#endif
		#endregion

	}
}
