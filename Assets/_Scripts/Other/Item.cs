using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Other
{
	[ExecuteInEditMode]
	public abstract class Item : ScriptableObject
	{
		public string displayName;
		[TextArea] public string description;

		private void OnEnable()
		{
			UpdateDisplayName();
		}

		public void UpdateDisplayName()
		{
			if (displayName == "" && name != "")
			{
				Debug.Log($"Set visible name to '{name}'", this);
				displayName = name;
			}
		}

		public override string ToString()
		{
			return name;
		}

		public abstract void AddToInventory(uint quantity);

		
	}
}
