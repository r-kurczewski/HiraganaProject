using Hiragana.Battle;
using System;
using UnityEditor;
using UnityEngine;

namespace Hiragana.Editors
{
	[CustomPropertyDrawer(typeof(Enemy.LifeSegment))]
	public class LifeSegmentDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 24f;
			//return base.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var indent = EditorGUI.indentLevel;
			var labelWidth = EditorGUIUtility.labelWidth;
			EditorGUI.indentLevel = 0;
			EditorGUIUtility.labelWidth = 70;

			var style = new GUIStyle();
			style.fontSize = 15;

			var labelRect = new Rect(position.width - 140, position.y + 4, position.width-60, 16);
			var enumRect = new Rect(position.width - 90, position.y + 4, 40, 16);
			var hiraganaRect = new Rect(position.width - 40, position.y + 2, 20, 16);
			var boolRect = new Rect(position.width - 20, position.y + 4, 20, 16);

			GUI.Label(labelRect, new GUIContent("Romaji"));
			EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("letter"), GUIContent.none);
			GUI.Label(hiraganaRect, ((Enemy.Hiragana)property.FindPropertyRelative("letter").intValue).ToString(), style);
			EditorGUI.PropertyField(boolRect, property.FindPropertyRelative("damaged"), GUIContent.none);

			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}