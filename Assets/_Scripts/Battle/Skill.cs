using Hiragana.Battle.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hiragana.Battle
{
	[Serializable]
	public abstract class Skill : MonoBehaviour
	{
		public abstract string Name { get; }
		public abstract int FocusCost { get; }
		public abstract SkillType Type { get; }

		protected abstract IEnumerator Effect();

		public void Use()
		{
			StartCoroutine(Effect());
		}

		protected void ReturnToSkillMenu()
		{
			var skillsMenu = FindObjectOfType<SkillsMenu>(true);
			var skillButtons = skillsMenu.gameObject.GetComponentsInChildren<SkillButton>();
			skillsMenu.Show();
			skillButtons.FirstOrDefault(x => x.skill == this).Select();
		}

		public enum SkillType { Offensive, Defensive, Buff }
	}
}