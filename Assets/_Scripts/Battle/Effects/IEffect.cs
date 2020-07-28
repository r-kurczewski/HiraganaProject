using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hiragana.Battle.Effects
{
	public abstract class Effect
	{
		public enum Target { Player, Self, Ally, Allies, AllEnemies };
		public enum EffectType { Damage, Heal, Poison, Slowness, Vulnerablity }

		void Execute();
	}
}
