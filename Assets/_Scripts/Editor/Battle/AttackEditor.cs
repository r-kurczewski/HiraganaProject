//using Hiragana.Battle;
//using Hiragana.Battle.Effects;
//using UnityEditor;
//using UnityEngine;
//using static Hiragana.Battle.Attack;
//using static Hiragana.Battle.Attack.TargetType;

//namespace Hiragana.Battle
//{
//	[CustomEditor(typeof(Attack))]
//	public class AttackEditor : Editor
//	{
//		public override void OnInspectorGUI()
//		{
//			EditorGUILayout.BeginHorizontal();

//			if (GUILayout.Button("Damage"))
//			{
//				AddEffect(TargetType.Player, new Damage());
//			}

//			if (GUILayout.Button("Heal"))
//			{
//				AddEffect(Self, new Heal());
//			}

//			if (GUILayout.Button("Poison"))
//			{
//				AddEffect(TargetType.Player, new Poison());
//			}

//			EditorGUILayout.EndHorizontal();
//			EditorGUILayout.BeginHorizontal();

//			if (GUILayout.Button("Slowness"))
//			{
//				AddEffect(TargetType.Player, new Slowness());
//			}

//			if (GUILayout.Button("Stun"))
//			{
//				AddEffect(TargetType.Player, new Stun());
//			}

//			if (GUILayout.Button("Thorns"))
//			{
//				AddEffect(Self, new Thorns());
//			}

//			EditorGUILayout.EndHorizontal();
//			EditorGUILayout.BeginHorizontal();

//			if (GUILayout.Button("Invincibility"))
//			{
//				AddEffect(Self, new Invincibility());
//			}

//			if (GUILayout.Button("Blindness"))
//			{
//				AddEffect(TargetType.Player, new Blindness());
//			}

//			EditorGUILayout.EndHorizontal();
//			base.OnInspectorGUI();
			
//		}

//		public void AddEffect<T>(TargetType targetType, T effect) where T : Effect
//		{
//			var attack = (Attack)target;
//			attack.effects.Add(new TargetedEffect(targetType, effect));
//			serializedObject.ApplyModifiedProperties();
//		}
//	}
//}