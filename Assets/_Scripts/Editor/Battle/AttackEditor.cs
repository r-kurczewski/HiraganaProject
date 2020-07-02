using Hiragana.Battle;
using UnityEditor;

namespace Hiragana.Editors
{
	[CustomEditor(typeof(Attack))]
	public class AttackEditor : Editor
	{
		void OnEnable()
		{
			var displayName = serializedObject.FindProperty("displayName");

			if (displayName.stringValue == "")
			{
				var name = serializedObject.FindProperty("m_Name");
				serializedObject.FindProperty("displayName").stringValue = name.stringValue;
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}