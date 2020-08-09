﻿using Hiragana.Battle.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Hiragana.Battle.Enemy.Romaji;
using Random = UnityEngine.Random;

namespace Hiragana.Battle
{
	public partial class Enemy : MonoBehaviour, IBattleTarget
	{

		public EnemyType type;
		[SerializeField] private int _speed;
		[SerializeField] private bool _skipTurn;
		[SerializeField] private List<LifeSegment> _health = new List<LifeSegment>();
		[SerializeField][SerializeReference] private List<EnemyStatus> statuses = new List<EnemyStatus>();

		public List<LifeSegment> Health { get => _health; set => _health = value; }
		public List<LifeSegment> CurrentHealth { get => Health.Where(e => !e.damaged).ToList(); }
		public int Speed { get => _speed; set => _speed = value; }
		public EnemySprite Sprite { get; private set; }
		public bool SkipTurn { get => _skipTurn; set => _skipTurn = value; }
		public bool Alive => CurrentHealth.Count > 0;
		public string Name => type.name;

		void Start()
		{
			Speed = type.baseSpeed;
			Sprite = GetComponent<EnemySprite>();
			List<Romaji> knownLetters = new List<Romaji> { A, I, U, E, ZU, DZU, DJI, JI };
			for (int i = 0; i < type.baseHealth; i++)
			{
				Romaji letter = knownLetters[Random.Range(0, knownLetters.Count)];
				Health.Add(new LifeSegment(letter));
				knownLetters.Remove(letter);
			}
			Sprite.UpdateLifeString();
		}

		public bool AddStatus(Status status)
		{
			var enStatus = status as EnemyStatus;
			var oldStatus = statuses.FirstOrDefault(x => x.GetType() == enStatus.GetType());
			if(oldStatus is null)
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
			var st = status as EnemyStatus;
			return statuses.Remove(st);

		}

		public void ApplyEffect(Effect effect)
		{
			effect.Apply(this);
		}

		public bool HaveStatus(Status status)
		{
			var st = status as EnemyStatus;
			return statuses.Contains(st);
		}

		public void ExecuteStatuses()
		{
			foreach (var status in statuses)
			{
				status.Execute(this);
			}
			statuses = statuses.Where(s => s.Keep).ToList();

			foreach (var life in Health)
			{
				life.status?.Execute(this);
			}
		}
	}

}
