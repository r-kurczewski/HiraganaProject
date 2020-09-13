//using Hiragana.Battle;
//using Hiragana.Battle.Effects;
//using UnityEditor;
//using UnityEngine;

//namespace Hiragana.Battle
//{
//	[CustomEditor(typeof(Potion))]
//	public class PotionEditor : Editor
//	{
//		public override void OnInspectorGUI()
//		{
//			EditorGUILayout.BeginHorizontal();

//			if (GUILayout.Button("Damage"))
//			{
//				AddEffect(new Heal());
//			}

//			if (GUILayout.Button("Heal"))
//			{
//				AddEffect(new Heal());
//			}

//			if (GUILayout.Button("Poison"))
//			{
//				AddEffect(new Poison());
//			}

//			EditorGUILayout.EndHorizontal();
//			EditorGUILayout.BeginHorizontal();

//			if (GUILayout.Button("Slowness"))
//			{
//				AddEffect(new Slowness());
//			}

//			if (GUILayout.Button("Stun"))
//			{
//				AddEffect(new Stun());
//			}

//			if (GUILayout.Button("Thorns"))
//			{
//				AddEffect( new Thorns());
//			}

//			EditorGUILayout.EndHorizontal();
//			EditorGUILayout.BeginHorizontal();

//			if (GUILayout.Button("Invincibility"))
//			{
//				AddEffect(new Invincibility());
//			}

//			if (GUILayout.Button("Blindness"))
//			{
//				AddEffect(new Blindness());
//			}

//			EditorGUILayout.EndHorizontal();
//			base.OnInspectorGUI();
			
//		}

//		public void AddEffect(Effect effect)
//		{
//			var potion = (Potion)target;
//			potion.effects.Add(effect);
//			serializedObject.ApplyModifiedProperties();
//		}
//	}
//}