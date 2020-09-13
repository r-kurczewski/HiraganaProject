using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Hiragana.Battle.Effects;
using UnityEngine;
using System.Linq;
using static Hiragana.Battle.Item;

namespace Hiragana.Battle
{

	public class Player : MonoBehaviour, IBattleTarget
	{
		public static Player player;

		[SerializeField] private int _health;
		[SerializeField] private int _maxHealth;
		[SerializeField] private int _focus;
		[SerializeField] private int _maxFocus;
		[SerializeField] private int _speed;
		[SerializeField] private int _turnProgress;
		[SerializeField] public float damageResistance;
		public bool haveTurn;

		[SerializeReference] public List<Skill> skills;
		[SerializeReference] public List<PlayerStatus> statuses = new List<PlayerStatus>();
		public List<ItemQuantity> items = new List<ItemQuantity>();

		public int Health { get => _health; set => _health = Mathf.Clamp(value, 0, MaxHealth); }
		public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
		public int Focus { get => _focus; set => _focus = Mathf.Clamp(value, 0, MaxFocus); }
		public int MaxFocus { get => _maxFocus; set => _maxFocus = value; }
		public int Speed { get => _speed; set => _speed = value; }
		public int TurnProgress { get => _turnProgress; set => _turnProgress = value; }

		public bool Alive => Health > 0;
		public string Name => "Player";
		public bool SkipTurn { get; set; }

		void Awake()
		{
			player = this;
		}

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

		public bool RemoveStatus<T>() where T: Status 
		{
			return statuses.Remove(statuses.FirstOrDefault(x=> x.GetType() == typeof(T)));
		}

		public void ApplyEffect(Effect effect)
		{
			effect.Apply(this);
		}

		public bool HaveStatus<T>() where T : Status
		{
			return statuses.Any(x => x.GetType() == typeof(T));
		}

		internal void RefreshItems()
		{
			items = items.Where(x => x.quantity > 0).ToList();
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