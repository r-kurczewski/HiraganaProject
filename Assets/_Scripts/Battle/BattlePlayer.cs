using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Hiragana.Battle.Effects;
using UnityEngine;
using System.Linq;
using static Hiragana.Battle.BattleItem;
using Hiragana.Other;

namespace Hiragana.Battle
{

	public class BattlePlayer : MonoBehaviour, IBattleTarget
	{
		public static BattlePlayer player;

		[SerializeField] private int _health;
		[SerializeField] private int _maxHealth;
		[SerializeField] private int _focus;
		[SerializeField] private int _maxFocus;
		[SerializeField] private int _speed;
		[SerializeField] private int _turnProgress;
		[SerializeField] public float damageResistance;
		public bool haveTurn;

		[SerializeReference][SerializeReferenceButton] public List<Skill> skills;

		[SerializeReference] public List<PlayerStatus> statuses = new List<PlayerStatus>();

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
			if (player)
			{
				Destroy(gameObject);
			}
			else
			{
				player = this;
				DontDestroyOnLoad(gameObject);
			}
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
			var status = statuses.FirstOrDefault(x => x.GetType() == typeof(T));
			status.Keep = false;
			return statuses.Remove(status);
		}

		public void ApplyEffect(Effect effect)
		{
			effect.Apply(this);
		}

		public bool HaveStatus<T>() where T : Status
		{
			return statuses.Any(x => x.GetType() == typeof(T));
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