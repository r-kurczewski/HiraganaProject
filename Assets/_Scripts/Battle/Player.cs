using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Hiragana.Battle.Effects;
using UnityEngine;
using System.Linq;

namespace Hiragana.Battle
{
	public class Player : MonoBehaviour, IBattleTarget
	{
		[SerializeField] private int _health;
		[SerializeField] private int _maxHealth;
		[SerializeField] private int _speed;
		[SerializeField] private bool _skipTurn;
		public bool haveTurn;

		[SerializeReference] private List<PlayerStatus> statuses = new List<PlayerStatus>();

		public int Health { get => _health; set => _health = Mathf.Clamp(value, 0, MaxHealth); }
		public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
		public int Speed { get => _speed; set => _speed = value; }
		public bool SkipTurn { get => _skipTurn; set => _skipTurn = value; }
		public bool Alive => Health > 0;
		public string Name => "Player";

		public bool AddStatus(Status status)
		{
			var enStatus = status as PlayerStatus;
			var oldStatus = statuses.FirstOrDefault(x => x.GetType() == enStatus.GetType());
			if (oldStatus is null)
			{
				statuses.Add(enStatus);
			}
			else
			{
				oldStatus.Merge(status);
			}
			return true;
		}

		public bool RemoveStatus(Status status)
		{
			var st = status as PlayerStatus;
			return statuses.Remove(st);
		}

		public void ApplyEffect(Effect effect)
		{
			effect.Apply(this);
		}

		public bool HaveStatus(Status status)
		{
			var st = status as PlayerStatus;
			return statuses.Contains(st);
		}

		public void ExecuteStatuses()
		{
			foreach (var status in statuses)
			{
				status.Execute(this);
			}
			statuses = statuses.Where(s => s.Keep).ToList();
		}
	}
}