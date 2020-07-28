using Hiragana.Battle.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Hiragana.Battle.Enemy.Romaji;
using static Hiragana.Battle.Enemy.Hiragana;
using Random = UnityEngine.Random;
using System;
using Hiragana.Battle.Effects;

namespace Hiragana.Battle
{
	public class Enemy : MonoBehaviour, IBattleTarget
	{

		public EnemyType type;
		[SerializeField] private List<LifeSegment> _health = new List<LifeSegment>();
		[SerializeField] private int _speed;

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
			Sprite.UpdateLife();
		}

		public bool ApplyDamage(int value)
		{
			throw new NotImplementedException();
		}

		public bool AddStatus(IStatus status)
		{
			throw new NotImplementedException();
		}

		public void RemoveStatus(IStatus status)
		{
			throw new NotImplementedException();
		}

		[Serializable]
		public class LifeSegment
		{
#pragma warning disable IDE0044
			[SerializeField] private Romaji letter;
#pragma warning restore IDE0044

			public bool damaged = false;

			public Hiragana Hiragana { get => (Hiragana)letter; }
			public Romaji Romaji { get => letter; }

			public LifeSegment(Romaji letter)
			{
				this.letter = letter;
			}

			public LifeSegment(Hiragana letter)
			{
				this.letter = (Romaji)letter;
			}

			public override string ToString()
			{
				return $"{Romaji} ({Hiragana}) [{(damaged ? "X" : " ")}]";
			}
		}

		public enum Romaji
		{
			A, I, U, E, O, N,
			KA, KI, KU, KE, KO,
			GA, GI, GU, GE, GO,
			SA, SHI, SU, SE, SO,
			ZA, JI, ZU, ZE, ZO,
			TA, CHI, TSU, TE, TO,
			DA, ji, zu, DE, DO,
			NA, NI, NU, NE, NO,
			HA, HI, FU, HE, HO,
			BA, BI, BU, BE, BO,
			PA, PI, PU, PE, PO,
			MA, MI, MU, ME, MO,
			RA, RI, RU, RE, RO,
			YA, YU, YO, WA, WO,
		}

		public enum Hiragana
		{
			あ, い, う, え, お, ん,
			か, き, く, け, こ,
			が, ぎ, ぐ, げ, ご,
			さ, し, す, せ, そ,
			ざ, じ, ず, ぜ, ぞ,
			た, ち, つ, て, と,
			だ, ぢ, づ, で, ど,
			な, に, ぬ, ね, の,
			は, ひ, ふ, へ, ほ,
			ば, び, ぶ, べ, ぼ,
			ぱ, ぴ, ぷ, ぺ, ぽ,
			ま, み, む, め, も,
			ら, り, る, れ, ろ,
			や, ゆ, よ, わ, を,
		}
	}

}
