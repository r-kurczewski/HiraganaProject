using Hiragana.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Hiragana.Battle.Attack;
using Random = UnityEngine.Random;
using UnityEngine;
using static Hiragana.Other.MyExtensions;

namespace Assets._Scripts.Battle
{
	public class EnemyTurn : ITurn
	{
		private Enemy self;
		private BattleScript Script { get; set; }
		private string EnemyName { get => self.type.name; }

		public EnemyTurn(Enemy self, BattleScript script)
		{
			this.self = self;
			Script = script;
		}

		public IEnumerator Execute()
		{
			Attack picked = PickAttack(self.type.moves);
			string msg = $"{EnemyName} uses {picked.name}.";
			Script.Log.Write(msg);
			Debug.Log(msg);
			foreach (var tarEffect in picked.effects)
			{
				foreach (var target in PickTargets(tarEffect))
				{
					tarEffect.effect.Apply(target);
				}
			}
			yield return null;
		}

		private Attack PickAttack(List<Move> moves)
		{
			Attack chosen = null;
			int totalPriority = (int)moves.Sum(e => e.priority);
			int rand = Random.Range(0, totalPriority);
			uint currentPriority = 0;
			for (int i = 0; i < moves.Count; i++)
			{
				if (currentPriority + moves[i].priority > rand)
				{
					chosen = moves[i].attack;
					break;
				}
				else
				{
					currentPriority += moves[i].priority;
				}
			}
			if(chosen == null) throw new InvalidOperationException($"{EnemyName} could not pick an attack.");
			return chosen;
		}


		private List<IBattleTarget> PickTargets(TargetedEffect effect)
		{
			var targets = new List<IBattleTarget>();
			if (effect.target == TargetType.Self)
			{
				targets.Add(self);
			}
			else if (effect.target == TargetType.Player)
			{
				targets.Add(Script.Player);
			}
			else if (effect.target == TargetType.Ally)
			{
				// TODO improve chosing target based on effect type
				targets.Add(Script.Enemies.Where(x => x != self).ToList().GetRandom());
			}
			else if (effect.target == TargetType.Allies)
			{
				targets.AddRange(Script.Enemies.Where(e => e != self));
			}
			else if (effect.target == TargetType.AllyAndSelf)
			{
				targets.AddRange(Script.Enemies);
			}
			else if (effect.target == TargetType.AllEnemies)
			{
				targets.AddRange(Script.Enemies);
			}
			else if (effect.target == TargetType.All)
			{
				targets.Add(Script.Player);
				targets.AddRange(Script.Enemies);
			}
			else
			{
				throw new NotImplementedException("Unsupported TargetType.");
			}
			return targets;
		}

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
