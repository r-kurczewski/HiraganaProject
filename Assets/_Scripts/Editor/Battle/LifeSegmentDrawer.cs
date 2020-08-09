using Hiragana.Battle;
using System;
using UnityEditor;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Editors
{
	[CustomPropertyDrawer(typeof(LifeSegment))]
	public class LifeSegmentDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 24 + property.FindPropertyRelative("status").CountInProperty() * 22;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position.y += 4; // centering
			position = EditorGUI.PrefixLabel(new Rect(position.x, position.y, position.width, 16), GUIUtility.GetControlID(FocusType.Passive), label);


			var indent = EditorGUI.indentLevel;
			var labelWidth = EditorGUIUtility.labelWidth;
			EditorGUI.indentLevel = 0;
			EditorGUIUtility.labelWidth = labelWidth - 20;

			var style = new GUIStyle
			{
				fontSize = 15
			};

			var enumRect = new Rect(position.x, position.y, 50, 16);
			var hiraganaRect = new Rect(position.x + 60, position.y, 20, 16);
			var boolRect = new Rect(position.x + 90, position.y, 20, 16);
			var statusRect = new Rect(Mathf.Max(position.x + 110 + (position.width - 180) / 2, position.x + 120), position.y, 70, 16);
			var testRect = new Rect(position.x, position.y + 24, position.width - 20, 16);
			;
			EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("letter"), GUIContent.none);
			GUI.Label(hiraganaRect, ((Enemy.Hiragana)property.FindPropertyRelative("letter").intValue).ToString(), style);
			EditorGUI.PropertyField(boolRect, property.FindPropertyRelative("damaged"), GUIContent.none);
			string status;
			try { status = property.FindPropertyRelative("status.type").stringValue; } catch { status = "No status"; }
			GUI.Label(statusRect, status);
			EditorGUI.PropertyField(testRect, property.FindPropertyRelative("status"), true);

			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}