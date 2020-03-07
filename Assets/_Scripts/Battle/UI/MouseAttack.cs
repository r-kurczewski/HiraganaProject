using System;
using System.Collections;
using UnityEngine;

namespace Hiragana.Battle.UI
{
	public class MouseAttack : IChooseAttackTarget
	{
		public AttackMenu Menu { get; set; }
		public EnemyList Enemies { get; set; }

		public MouseAttack(AttackMenu menu, EnemyList enemies)
		{
			Menu = menu;
			Enemies = enemies;
		}

		public IEnumerator ChooseEnemy()
		{
			Menu.keyListening = false;
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
			Menu.keyListening = true;
		}

		public IEnumerator TypeHiragana()
		{
			throw new NotImplementedException();
		}

		public IEnumerator ConfirmAttack()
		{
			throw new NotImplementedException();
		}
	}
}
