using System;
using System.Text;
using UnityEngine;

namespace Hiragana.Battle
{
	public partial class Enemy
	{
		[Serializable]
		public class LifeSegment
		{
			#pragma warning disable IDE0044
			[SerializeField] private Romaji letter;
			#pragma warning restore IDE0044

			public bool damaged = false;

			[SerializeReference] public HiraganaStatus status;

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

			public string GetFormatingString()
			{
				string str = damaged ? $"<alpha=#44>{Hiragana.ToString()}<alpha=#ff>" : Hiragana.ToString();
				if(status != null) str = status.GetStatusFormating(str);
				return str;
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
			DA, DJI, DZU, DE, DO,
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
