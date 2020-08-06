using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Hiragana.Battle.Effects;
using UnityEngine;

namespace Hiragana.Battle
{
	public class Player : MonoBehaviour, IBattleTarget
	{
		[SerializeField] private int _health;
		[SerializeField] private int _maxHealth;
		[SerializeField] private int _speed;
		public bool haveTurn;

		[SerializeReference] private List<PlayerStatus> statuses = new List<PlayerStatus>();

		public int Health { get => _health;  set => _health = Mathf.Clamp(value, 0, MaxHealth); }
		public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
		public int Speed { get => _speed; private set => _speed = value; }

		public bool AddStatus(Status status)
		{
			var st = TryCastStatus(status);
			statuses.Add(st);
			return true;
		}

		public bool RemoveStatus(Status status)
		{
			var st = TryCastStatus(status);
			return statuses.Remove(st);

		}

		public bool ApplyEffect(Effect effect)
		{
			return effect.Apply(this);
		}

		public bool HaveStatus(Status status)
		{
			var st = TryCastStatus(status);
			return statuses.Contains(st);
		}

		private PlayerStatus TryCastStatus(Status status)
		{
			if (status is PlayerStatus)
			{
				return status as PlayerStatus;
			}
			else
			{
				throw new InvalidOperationException("This target is not compatible with this type of status.");
			}
		}
	}
}