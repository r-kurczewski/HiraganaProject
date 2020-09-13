using Hiragana.Battle;
using Hiragana.Battle.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;

public class BattleScript : MonoBehaviour
{
	public static BattleScript script;

#pragma warning disable IDE0044, 0649
	[SerializeField] private PlayerPanel playerGUI;
	[SerializeField] private EnemyScreen enemyGUI;
	[SerializeField] private BattleLog log;
	[SerializeField] private List<Enemy> enemies;
#pragma warning restore IDE0044, 0649

	private IEnumerator coroutine;

	public List<Enemy> Enemies { get => enemies; }

	void Start()
	{
		script = this;
	}

	public IEnumerable LoadBattle(Encounter encounter)
	{
		enemies = EnemyScreen.context.LoadEncounter(encounter);
		yield return new WaitWhile(() => enemies is null);
		var turns = TurnQueue(enemies).GetEnumerator();
		UpdateGUI();
		BattleLog.log.SetVisibility(true);
		yield return new WaitForSeconds(1);
		coroutine = TurnManager(turns);
		StartCoroutine(coroutine);
	}

	IEnumerator TurnManager(IEnumerator<Turn> turns)
	{
		while (turns.MoveNext()) // for each turn
		{
			Turn turn = turns.Current;
			if (turn.Target.Alive == false) continue;

			turn.Target.ExecuteStatuses();
			UpdateGUI();
			CheckWinLoseConditions();
			var turnEnum = turn.Execute();

			while (turnEnum.MoveNext()) // wait till turn executed
			{
				yield return new WaitForEndOfFrame();
			}

			UpdateGUI();
			CheckWinLoseConditions();
			yield return new WaitForSeconds(1); // delay between turns
		}
	}

	IEnumerable<Turn> TurnQueue(List<Enemy> enemies)
	{
		var playerTurn = new PlayerTurn(Player.player);
		List<Turn> turns = new List<Turn>() { playerTurn };
		foreach (var enemy in enemies)
		{
			turns.Add(new EnemyTurn(enemy));
		}

		while (turns.Count > 0)
		{
			turns = turns.Where(e => e.Target.Alive).OrderByDescending(e => e.Target.Speed).ToList();
			foreach (var turn in turns)
			{
				turn.Recalculate();
				while (turn.Active())
				{
					yield return turn;
				}
			}
		}
	}

	private void CheckWinLoseConditions()
	{
		if (Player.player.Health <= 0) // you lose
		{
			StopCoroutine(coroutine);
			script.log.Write("You lose");
		}
		else if (enemies.Where(e => e.CurrentHealth.Count > 0).Count() == 0) // you win
		{
			StopCoroutine(coroutine);
			script.log.Write("You win");
		}
	}

	private void UpdateGUI()
	{
		playerGUI.UpdateGUI();
		enemyGUI.UpdateGUI();
	}
}
