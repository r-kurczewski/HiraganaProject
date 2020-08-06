using Hiragana.Battle.UI;
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
		[SerializeField] private List<LifeSegment> _health = new List<LifeSegment>();
		[SerializeField] private List<EnemyStatus> statuses = new List<EnemyStatus>();

		public List<LifeSegment> Health { get => _health; set => _health = value; }
		public List<LifeSegment> CurrentHealth { get => Health.Where(e => !e.damaged).ToList(); }
		public int Speed { get => _speed; private set => _speed = value; }
		public EnemySprite Sprite { get; private set; }

		void Start()
		{
			Speed = type.baseSpeed;

			Sprite = GetComponent<EnemySprite>();

			List<Romaji> knownLetters = new List<Romaji> { A, I, U, E, O, N, };
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

		private EnemyStatus TryCastStatus(Status status)
		{
			if(status is EnemyStatus)
			{
				return status as EnemyStatus;
			}
			else
			{
				throw new InvalidOperationException("This target is not compatible with this type of status.");
			}
		}
	}

}
