using Hiragana.Battle;
using UnityEditor;
using UnityEngine;

namespace Hiragana.Editors
{
	[CustomPropertyDrawer(typeof(Encounter.EnemyPosition))]
	public class EnemyPositionDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 24f;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var enemyRect = new Rect(position.x, position.y + 4, position.width - 115, 16);
			var posRect = new Rect(position.width - 95, position.y + 4, 95, 16);

			EditorGUI.PropertyField(enemyRect, property.FindPropertyRelative("enemy"), GUIContent.none);
			EditorGUI.PropertyField(posRect, property.FindPropertyRelative("pos"), GUIContent.none);

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}