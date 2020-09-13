using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Hiragana.Battle.Skill.SkillType;

namespace Hiragana.Battle.UI
{
	public class SkillButton : Button
	{
		#pragma warning disable 0649
		[SerializeField] private TMP_Text label;
		[SerializeField] private Transform costBox;
		[SerializeField] private Image watermark;
		public Skill skill;
		#pragma warning restore 0649

		protected override void Start()
		{
			base.Start();
			label.text = name = skill.Name;
			for (int i = 0; i < skill.FocusCost; i++)
			{
				Instantiate(Resources.Load("_Prefabs/UI/FocusPoint"), costBox);
			}
			onClick.AddListener(() => skill.Use());

			switch (skill.Type)
			{
				case Offensive:
					watermark.sprite = Resources.Load<Sprite>("offensive");
					break;

				case Defensive:
					break;

				case Buff:
					watermark.sprite = Resources.Load<Sprite>("buff");
					break;
			}
		}

		public static SkillButton Create(Skill skill, Transform skillsParent)
		{
			var button = Instantiate(Resources.Load<GameObject>("_Prefabs/UI/PlayerSkill"), skillsParent).GetComponent<SkillButton>();
			button.skill = skill;
			return button;
		}
	}
}