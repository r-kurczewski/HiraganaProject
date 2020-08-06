using Hiragana.Battle.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hiragana.Battle
{
	[ExecuteInEditMode]
	[CreateAssetMenu(fileName = "Attack", menuName = "Battle/Attack", order = 1)]
	public class Attack : ScriptableObject
	{
		public string displayName;
		public List<TargetedEffect> effects = new List<TargetedEffect>();

		public void UpdateDisplayName()
		{
			if (displayName == "" && name != "")
			{
				Debug.Log($"Set visible name to '{name}'", this);
				displayName = name;
			}
		}

		private void OnEnable()
		{
			UpdateDisplayName();
		}

		public override string ToString()
		{
			return name;
		}

		public enum TargetType { Self, Player, Ally, Allies, AllyAndSelf, AllEnemies, All,  }

		[Serializable]
		public class TargetedEffect
		{
			private string name;
			public TargetType target;
			[SerializeReference] public Effect effect;

			public TargetedEffect(TargetType target, Effect effect)
			{
				this.target = target;
				this.effect = effect;
			}
		}

	}
}
