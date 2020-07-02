using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hiragana.Battle
{
	[Serializable]
	public class BattleEffect
	{
		public Status status;
		public Target target;
		public int value;

		public enum Target { Player, Self, Ally, Allies};
		public enum Status { Damage, Heal, Poison, Slowness, Vulnerablity}
	}
}
