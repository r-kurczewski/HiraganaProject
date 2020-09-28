using Hiragana.Battle;
using Hiragana.Battle.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Hiragana.World;

public class BattleScript : MonoBehaviour
{
	public static Encounter currentEncounter;
	public static BattleScript script;

#pragma warning disable IDE0044, 0649
	[SerializeField] private PlayerPanel playerGUI;
	[SerializeField] private EnemyScreen enemyGUI;
	[SerializeField] private BattleLog log;
	[SerializeField] private Encounter debugEncounter;
	[SerializeField] private List<Enemy> enemies;
#pragma warning restore IDE0044, 0649

	private IEnumerator coroutine;

	public List<Enemy> Enemies { get => enemies; }

	void Start()
	{
		script = this;
		UpdateGUI();
		StartCoroutine(LoadBattle(currentEncounter ?? debugEncounter));
	}

	public IEnumerator LoadBattle(Encounter encounter)
	{
		enemies = EnemyScreen.context.LoadEncounter(encounter);
		yield return new WaitWhile(() => enemies is null);
		var turns = TurnQueue(enemies).GetEnumerator();
		UpdateGUI();
		BattleLog.log.SetVisibility(true);
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
		var playerTurn = new PlayerTurn(BattlePlayer.player);
		List<Turn> turns = new List<Turn>() { playerTurn };
		foreach (var enemy in enemies)
		{
			turns.Add(new EnemyTurn(enemy));
		}

		bool firstTurn = true;
		yield return playerTurn; // player always start first
		while (turns.Count > 0)
		{
			turns = turns.Where(e => e.Target.Alive).OrderByDescending(e => e.Target.Speed).ToList();
			foreach (var turn in turns)
			{
				if (firstTurn && turn is PlayerTurn)
				{
					firstTurn = false;
					continue; // skip player turn to avoid double turn
				}
				turn.Recalculate();
				while (turn.Active()) yield return turn;
			}
		}
	}

	private void CheckWinLoseConditions()
	{
		if (BattlePlayer.player.Health <= 0) // you lose
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

	public void ReturnToWorld()
	{
		SceneManager.LoadScene(WorldPlayer.locationId);
		WorldPlayer.player.Activate();
	}

	private void UpdateGUI()
	{
		playerGUI.UpdateGUI();
		enemyGUI.UpdateGUI();
	}
}
