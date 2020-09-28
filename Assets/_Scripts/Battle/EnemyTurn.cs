using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Hiragana.Battle.Attack;
using Random = UnityEngine.Random;
using UnityEngine;
using static Hiragana.Other.MyExtensions;

namespace Hiragana.Battle
{
	public class EnemyTurn : Turn
	{
		private Enemy self;
		public override IBattleTarget Target { get => self; }

		public EnemyTurn(Enemy self)
		{
			this.self = self;
		}

		public override IEnumerator Execute()
		{
			if (Target.SkipTurn)
			{
				BattleLog.log.Write($"{self.Name} skips a turn.");
				Target.SkipTurn = false;
				yield break;
			}

			Attack picked = PickAttack(self.type.moves);
			string msg = $"{self.Name} uses {picked.name}.";
			BattleLog.log.Write(msg);
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
			if(chosen == null) throw new InvalidOperationException($"{self.Name} could not pick an attack.");
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
				targets.Add(BattlePlayer.player);
			}
			else if (effect.target == TargetType.Ally)
			{
				targets.Add(BattleScript.script.Enemies.Where(x => x != self).ToList().TryGetRandom());
			}
			else if (effect.target == TargetType.Allies)
			{
				targets.AddRange(BattleScript.script.Enemies.Where(e => e != self));
			}
			else if (effect.target == TargetType.AllyAndSelf)
			{
				targets.AddRange(BattleScript.script.Enemies);
			}
			else if (effect.target == TargetType.AllEnemies)
			{
				targets.AddRange(BattleScript.script.Enemies);
			}
			else if (effect.target == TargetType.All)
			{
				targets.Add(BattlePlayer.player);
				targets.AddRange(BattleScript.script.Enemies);
			}
			else
			{
				throw new NotImplementedException("Unsupported TargetType.");
			}
			return targets;
		}

	}
}
