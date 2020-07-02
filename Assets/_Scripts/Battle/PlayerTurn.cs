using System.Collections;
using UnityEngine;

namespace Hiragana.Battle
{
	public class PlayerTurn : MonoBehaviour, ITurn
	{
		private PlayerData data;


		public int GetSpeed()
		{
			return data.Speed;
		}

		public bool IsAlive()
		{
			return data.Health > 0;
		}

		public void Execute()
		{
			
		}
	}
}
