using Hiragana.Battle.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Hiragana.Battle.Enemy.Romaji;
using static Hiragana.Battle.Enemy.Hiragana;
using Random = UnityEngine.Random;

namespace Hiragana.Battle
{
	public class Enemy : MonoBehaviour
	{
		public HashSet<Romaji> health = new HashSet<Romaji>();
		public HashSet<Romaji> currentHealth = new HashSet<Romaji>();
		public EnemyType type;
		[SerializeField] private int _speed;

		public int Speed { get => _speed; private set => _speed = value; }
		public EnemySprite Sprite { get; private set; }

		void Start()
		{
			Sprite = GetComponent<EnemySprite>();

			List<Romaji> knownLetters = new List<Romaji> { A, I, U, E, O, N, };
			for (int i = 0; i < type.baseHealth; i++)
			{
				Romaji letter = knownLetters[Random.Range(0, knownLetters.Count)];
				health.Add(letter);
				knownLetters.Remove(letter);
			}
			currentHealth = new HashSet<Romaji>(health);
			currentHealth.Remove(currentHealth.First());
			currentHealth.Remove(currentHealth.Last());
			UpdateLife();

			Speed = type.baseSpeed;
		}

		public void UpdateLife()
		{
			Sprite.NameLabel.text = string.Empty;
			foreach (var letter in health)
			{
				if (currentHealth.Contains(letter))
				{
					Sprite.NameLabel.text += (Hiragana)letter;
				}
				else
				{
					Sprite.NameLabel.text += $"<color=#7774>{(Hiragana)letter}</color>";
				}
			}
		}

		//readonly Dictionary<Romaji, List<Hiragana>> translate = new Dictionary<Romaji, List<Hiragana>>
		//{
		//	{ JI, new List<Hiragana>{ じ, ぢ} },
		//};

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
