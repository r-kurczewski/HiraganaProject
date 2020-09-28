using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Hiragana.Battle.UI
{
	public class SkillsMenu : MenuOption
	{
		public Transform skillsParent;

		void Start()
		{
			foreach (var skill in BattlePlayer.player.skills)
			{
				SkillButton.Create(skill, skillsParent);
			}
		}

		new private void OnEnable()
		{
			StartCoroutine(SelectSkill());
		}

		private IEnumerator SelectSkill()
		{
			yield return new WaitUntil(() => skillsParent.childCount == 4);
			foreach (var button in skillsParent.GetComponentsInChildren<SkillButton>())
			{
				button.interactable = (button.skill.FocusCost <= BattlePlayer.player.Focus);
			}
			skillsParent.GetComponentsInChildren<SkillButton>().FirstOrDefault(x => x.interactable)?.Select();
		}
	}
}