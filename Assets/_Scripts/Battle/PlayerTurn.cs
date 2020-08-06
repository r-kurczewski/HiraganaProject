using System.Collections;
using UnityEngine;

namespace Hiragana.Battle
{
	public class PlayerTurn : ITurn
	{
		private Player player;
		private BattleScript Script { get; set; }

		public PlayerTurn(Player player, BattleScript script)
		{
			this.player = player;
			Script = script;
		}

		public IEnumerator Execute()
		{
			Debug.Log("Your turn.");
			while (player.haveTurn)
			{
				yield return null;
			}
			Debug.Log("End of your turn.");
		}

		public int GetSpeed()
		{
			return player.Speed;
		}

		public bool IsAlive()
		{
			return player.Health > 0;
		}

		public string GetName()
		{
			return "Player";
		}
	}
}
