using Hiragana.Other;
using UnityEditor;
using UnityEngine;
using static Hiragana.Battle.BattleItem;

namespace Hiragana.Editors
{
	[CustomPropertyDrawer(typeof(ItemQuantity<Item>))]
	public class ItemQuantityDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 24;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position.y += 4;
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			int quantityWidth = 30;
			var quantityRect = new Rect(position.x, position.y, quantityWidth, 16);
			var itemRect = new Rect(position.x + quantityWidth + 10, position.y, position.width - quantityWidth - 20, 16);

			EditorGUI.PropertyField(itemRect, property.FindPropertyRelative("item"), GUIContent.none);
			EditorGUI.PropertyField(quantityRect, property.FindPropertyRelative("quantity"), GUIContent.none);

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}

