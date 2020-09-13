using Hiragana.Battle.Effects;
using Hiragana.Battle.UI;
using System.Collections;
using UnityEngine;

namespace Hiragana.Battle
{
	public class PlayerTurn : Turn
	{
		private Player player;
		private BattleScript Script { get; set; }

		public override IBattleTarget Target => player;

		public PlayerTurn(Player player, BattleScript script)
		{
			this.player = player;
			Script = script;
		}

		public override IEnumerator Execute()
		{
			if (Target.SkipTurn)
			{
				BattleLog.log.Write($"{player.Name} skips a turn.");
				Target.SkipTurn = false;
				yield break;
			}

			BattleLog.log.SetVisibility(false);
			GameObject.FindObjectOfType<MainMenu>(true).Show();
			player.haveTurn = true;
			Debug.Log("Your turn.");
			while (player.haveTurn)
			{
				yield return null;
			}
			BattleLog.log.SetVisibility(true);
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
