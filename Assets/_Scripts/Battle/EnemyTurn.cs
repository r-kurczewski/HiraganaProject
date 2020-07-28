using Hiragana.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
//using static Hiragana.Battle.Effects.Effect.Target;
using Hiragana.Battle.Effects;

namespace Assets._Scripts.Battle
{
	public class EnemyTurn : ITurn
	{
		private Enemy self;
		protected BattleScript Script { get; set; }

		public EnemyTurn(Enemy self, BattleScript script)
		{
			this.self = self;
			Script = script;
		}

		public IEnumerator Execute()
		{
			List<Move> moves = self.type.moves;
			int totalPriority = (int)moves.Sum(e => e.priority);
			int rand = Random.Range(0, totalPriority);
			Attack chosen = default;

			uint current = 0;
			foreach (var move in moves)
			{
				if (current + move.priority > rand)
				{
					chosen = move.attack;
					break;
				}
				else
				{
					current += move.priority;
				}
			}
			Script.Log.Write($"{self.type.name} uses {chosen.name}.");
			foreach(var effect in chosen.effects)
			{
				//var targets = SetTargets(effect);
				//Script.ApplyEffect(effect, targets);
				
			}
			yield return null;
		}

		//private List<IBattleTarget> SetTargets(Effect effect)
		//{
		//	var targets = new List<IBattleTarget>();
		//	if (effect.target == Player)
		//	{
		//		targets.Add(Script.Player);
		//	}
		//	else if (effect.target == Self)
		//	{
		//		targets.Add(self);
		//	}
		//	else if (effect.target == Allies)
		//	{
		//		targets.AddRange(Script.Enemies.Where(e => e != self));
		//	}
		//	else if (effect.target == Ally)
		//	{
		//		// TODO improve chosing target based on effect type
		//		targets.Add(Script.Enemies[Random.Range(0, Script.Enemies.Count)]);
		//	}
		//	return targets;
		//}

		public int GetSpeed()
		{
			return self.Speed;
		}

		public bool IsAlive()
		{
			return self.CurrentHealth.Count > 0;
		}

		public string GetName()
		{
			return self.type.name;
		}
	}
}
