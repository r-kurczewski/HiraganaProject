using Assets._Scripts.Battle;
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
	[SerializeField] private Player playerData;
	[SerializeField] private PlayerPanel playerGUI;
	[SerializeField] private EnemyScreen enemyGUI;
	public BattleLog log;
	[SerializeField] private List<Enemy> enemies;
	#pragma warning restore IDE0044, 0649

	private IEnumerator turnManager;

	public Player Player { get => playerData; }
	public List<Enemy> Enemies { get => enemies; }

	void Start()
	{
		script = this;
	}

	public void LoadBattle(Encounter encounter)
	{
		enemies = EnemyScreen.context.LoadEncounter(encounter);
		var turns = TurnQueue(enemies).GetEnumerator();
		UpdateGUI();
		turnManager = TurnManager(turns);
		StartCoroutine(turnManager);
	}

	IEnumerator TurnManager(IEnumerator<ITurn> turns)
	{
		yield return new WaitForEndOfFrame(); // wait to assign all enemy data like life and speed.
		while (turns.MoveNext()) // for each turn
		{
			ITurn turn = turns.Current;
			turn.Target.ExecuteStatuses();
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

	private void CheckWinLoseConditions()
	{
		if (playerData.Health <= 0) // you lose
		{
			StopCoroutine(turnManager);
			script.log.Write("You lose");
		}
		else if (enemies.Where(e => e.CurrentHealth.Count > 0).Count() == 0) // you win
		{
			StopCoroutine(turnManager);
			script.log.Write("You win");
		}
	}

	private void UpdateGUI()
	{
		playerGUI.UpdateGUI();
		enemyGUI.UpdateGUI();
	}

	IEnumerable<ITurn> TurnQueue(List<Enemy> enemies)
	{
		var playerTurn = new PlayerTurn(playerData, this);
		List<ITurn> turns = new List<ITurn>() { playerTurn };
		foreach (var enemy in enemies)
		{
			turns.Add(new EnemyTurn(enemy));
		}

		while (turns.Count > 0)
		{
			turns = turns.Where(e => e.Target.Alive).OrderByDescending(e => e.Target.Speed).ToList();
			foreach (var turn in turns)
			{
				if (turn.Target.Alive) yield return turn;
				else continue;
			}
		}
	}
}
