using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class AttackMenu : MenuOption
	{
		public Button submitButton;
		public TMP_InputField textField;
		public EnemyList enemies;

		public KeyboardAttack keyboard;
		public MouseAttack mouse;

		public IChooseAttackTarget input;

		private void Awake()
		{
			input = keyboard;
		}

		protected void OnEnable()
		{
			StartCoroutine(input.ChooseEnemy());
		}

		private void OnDisable()
		{
			enemies.DisableSelection();
		}

		public override void OnEnter()
		{
			return;
		}
	}
}
