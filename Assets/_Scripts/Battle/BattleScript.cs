using Assets._Scripts.Battle;
using Hiragana.Battle;
using Hiragana.Battle.UI;
using Hiragana.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
	[SerializeField] private PlayerData playerTurn;

	public void LoadBattle(Encounter encounter)
	{
		var turns = TurnEnumerable(EnemyScreen.context.LoadEncounter(encounter));
		foreach (var turn in turns)
		{
			Debug.Log($"Now {turn.GetType().Name} turn");
			turn.Execute();
		}
	}

	public void EndTurn()
	{
		return;
	}

	IEnumerable<ITurn> TurnEnumerable(List<Enemy> enemies)
	{
		List<ITurn> allTurns = new List<ITurn>() { playerTurn };
		foreach (var enemy in enemies)
		{
			allTurns.Add(new EnemyTurn(enemy));
		}

		for (int i = 0; i < 1; i++) // do konca walki
		{
			allTurns.Where(e => e.IsAlive()).OrderBy(e => e.GetSpeed()).Reverse();
			foreach (var turn in allTurns)
			{
				yield return turn;
			}
		}
	}
}
