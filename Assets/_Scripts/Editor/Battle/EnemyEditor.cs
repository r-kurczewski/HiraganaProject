using Hiragana.Battle;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hiragana.Editors
{
	[CustomEditor(typeof(Enemy))]
	public class EnemyEditor : Editor
	{
		//public override void OnInspectorGUI()
		//{
		//	var enemy = (Enemy)target;
		//	List<string> romajiStr = new List<string>();
		//	List<string> hiraganaStr = new List<string>();
		//	foreach (var letter in enemy.Health)
		//	{
		//		romajiStr.Add(letter.Romaji.ToString());
		//		hiraganaStr.Add(letter.Hiragana.ToString());
		//	}

		//	EditorGUILayout.BeginHorizontal();
		//	EditorGUILayout.LabelField("Health", GUILayout.Width(115));
		//	EditorGUILayout.HelpBox(string.Join(" ", romajiStr), MessageType.None);
		//	EditorGUILayout.HelpBox(string.Join(" ", hiraganaStr), MessageType.None);
		//	EditorGUILayout.EndHorizontal();
		//	base.OnInspectorGUI();
		//}
	}
}