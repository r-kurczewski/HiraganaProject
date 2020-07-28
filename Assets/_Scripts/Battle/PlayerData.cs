using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Hiragana.Battle.Effects;
using UnityEngine;

namespace Hiragana.Battle
{
	public class PlayerData : MonoBehaviour, IBattleTarget
	{
		[SerializeField] private int _health;
		[SerializeField] private int _maxHealth;
		[SerializeField] private int _speed;
		public bool haveTurn;

		public PoisonState poison;

		public int Health { get => _health; private set => _health = Mathf.Clamp(value, 0, MaxHealth); }
		public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
		public int Speed { get => _speed; private set => _speed = value; }

		void Start()
		{
			poison = new PoisonState(this);
		}

		public bool AddStatus(IStatus status)
		{
			throw new NotImplementedException();
		}

		public void RemoveStatus(IStatus status)
		{
			throw new NotImplementedException();
		}

		public bool ApplyDamage(int value)
		{
			throw new NotImplementedException();
		}

		[Serializable]
		public class PoisonState : IBattleStatus
		{
			public int damage;
			PlayerData data;

			public PoisonState(PlayerData data)
			{
				this.data = data;
			}

			public void Add(int i)
			{
				damage += i;
			}

			public bool Apply()
			{
				if (damage > 0)
				{
					data.Health -= damage--;
					return true;
				}
				else return false;
			}

			public void Remove()
			{
				damage = 0;
			}
		}
	}
}