using Hiragana.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hiragana.Editors
{
	//[CustomPropertyDrawer(typeof(Effect))]
	public class EffectDrawer : PropertyDrawer
	{
		int fields = 0;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			fields = 0;
			EditorGUI.BeginProperty(position, label, property);
			//position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			//var indent = EditorGUI.indentLevel;
			//EditorGUI.indentLevel = 0;

			//var enemyRect = new Rect(position.x, position.y, position.width, 16);
			//var posRect = new Rect(position.x, position.y + 16, position.width / 2, 16);

			EditorGUI.PropertyField(GetPropertyRect(position, fields++), property.FindPropertyRelative("type"));
			EditorGUI.PropertyField(GetPropertyRect(position, fields++), property.FindPropertyRelative("target"));
			EditorGUI.PropertyField(GetPropertyRect(position, fields++), property.FindPropertyRelative("haveValue"));
			EditorGUI.PropertyField(GetPropertyRect(position, fields++), property.FindPropertyRelative("haveDuration"));

			if (property.FindPropertyRelative("haveValue").boolValue)
			{
				EditorGUI.PropertyField(GetPropertyRect(position, fields++), property.FindPropertyRelative("value"), new GUIContent("Value"));
			}
			if (property.FindPropertyRelative("haveDuration").boolValue)
			{
				EditorGUI.PropertyField(GetPropertyRect(position, fields++), property.FindPropertyRelative("duration"), new GUIContent("Duration"));
			}
			//EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}

		public Rect GetPropertyRect(Rect position, int i)
		{
			return new Rect(position.x, position.y + 16 * i, position.width, 16);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return fields * 16;
		}
	}
}