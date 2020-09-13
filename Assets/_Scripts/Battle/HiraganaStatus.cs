
using System.Linq;
using UnityEngine;
using static Hiragana.Battle.Enemy;
using static Hiragana.Other.MyExtensions;

namespace Hiragana.Battle
{
	public abstract class HiraganaStatus : Status
	{
		protected LifeSegment life;
		[SerializeField] protected int amount = 1;

		protected HiraganaStatus()
		{

		}

		public HiraganaStatus(HiraganaStatus org)
		{
			amount = org.amount;
		}

		public abstract string GetStatusFormating(string str);

		public abstract void OnHit();

		public override void Merge(Status newStatus)
		{
			Debug.LogWarning(GetType().Name + " skips merging.");
			return;
		}

		public override void Apply(IBattleTarget target)
		{
			for (int i = 0; i < amount; i++)
			{
				var enemy = target as Enemy;
				LifeSegment life = enemy.Health.Where(x => x.damaged is false && x.status is null).ToList().TryGetRandom();
				if (life != null)
				{
					var statusCopy = Clone() as HiraganaStatus;
					life.status = statusCopy;
					statusCopy.life = life;
				}
			}
		}

		public override void OnRemove()
		{
			life.status = null;
		}
	}
}