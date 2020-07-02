﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hiragana.Battle
{
	[CreateAssetMenu(fileName = "Enemy", menuName = "Battle/Enemy", order = 1)]
	[Serializable]
	public class EnemyType : ScriptableObject
	{
		public GameObject spritePrefab;
		public uint baseHealth;
		public int baseSpeed;
		public List<Move> moves = new List<Move>();
	}

	[Serializable]
	public class Move
	{
		public Attack attack;
		public uint chances;
	}
}
