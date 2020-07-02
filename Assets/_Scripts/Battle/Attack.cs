using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Hiragana.Battle
{
	[ExecuteInEditMode]
	[CreateAssetMenu(fileName = "Attack", menuName = "Battle/Attack", order = 1)]
	public class Attack : ScriptableObject
	{
		public string displayName;
		public List<BattleEffect> effects;


		public void UpdateDisplayName()
		{
			if (displayName == "" && name != "")
			{
				Debug.Log($"Set visible name to '{name}'", this);
				displayName = name;
			}
		}
	}
}
