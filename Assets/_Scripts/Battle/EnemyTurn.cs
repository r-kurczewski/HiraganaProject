using Hiragana.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets._Scripts.Battle
{
	public class EnemyTurn : ITurn
	{
		public Enemy Self { get; private set; }

		public EnemyTurn(Enemy self)
		{
			Self = self;
		}

		public void Execute()
		{
			Debug.Log($"{Self.type.name} waits. (pre)");
			Thread.Sleep(1000);
			Debug.LogWarning($"{Self.type.name} waits.");
		}

		public int GetSpeed()
		{
			return Self.Speed;
		}

		public bool IsAlive()
		{
			return Self.currentHealth.Count > 0;
		}
	}
}
