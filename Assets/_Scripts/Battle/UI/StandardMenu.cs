﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hiragana.Battle.UI
{
	public class StandardMenu : MenuOption
	{
		EnemyList enemies;

		private void Awake()
		{
			enemies = FindObjectOfType<EnemyList>();
		}

		private void OnEnable()
		{
			//enemies.DisableSelection(true);
			StartCoroutine(SelectButtton());
		}

		public IEnumerator SelectButtton()
		{
			yield return new WaitUntil(() => firstSelection ? firstSelection.isActiveAndEnabled : false);
			firstSelection.Select();
		}
	}
}
