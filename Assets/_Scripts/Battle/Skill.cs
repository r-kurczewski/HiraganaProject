using Hiragana.Battle.UI;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Hiragana.Battle
{
	[Serializable]
	public abstract class Skill
	{
		public abstract string Name { get; }
		public abstract int FocusCost { get; }
		public abstract SkillType Type { get; }

		protected abstract IEnumerator Effect();

		protected void ReturnToSkillMenu()
		{
			var skillsMenu = GameObject.FindObjectOfType<SkillsMenu>(true);
			var skillButtons = skillsMenu.gameObject.GetComponentsInChildren<SkillButton>();
			skillsMenu.Show();
			skillButtons.FirstOrDefault(x => x.skill == this).Select();
		}

		public void Use()
		{
			StartCoroutine(Effect());
		}

		protected Coroutine StartCoroutine(IEnumerator routine)
		{
			return BattlePlayer.player.StartCoroutine(routine);
		}

		public enum SkillType { Offensive, Defensive, Buff }
	}
}