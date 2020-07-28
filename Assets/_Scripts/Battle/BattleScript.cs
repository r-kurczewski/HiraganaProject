using Assets._Scripts.Battle;
using Hiragana.Battle;
using Hiragana.Battle.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;
using static Hiragana.Battle.Effects.Effect.EffectType;

public class BattleScript : MonoBehaviour
{
#pragma warning disable IDE0044, 0649
	[SerializeField] private PlayerData playerData;
	[SerializeField] private PlayerPanel playerGUI;
	[SerializeField] private EnemyScreen enemyGUI;
	[SerializeField] private StandardMenu battleMenu;
	[SerializeField] private BattleLog log;
	[SerializeField] private List<Enemy> enemies;
	private ITurn currentTurn;
	private IEnumerator<ITurn> turns;
#pragma warning restore IDE0044, 0649

	public PlayerData Player { get => playerData; }
	public List<Enemy> Enemies { get => enemies; }
	public BattleLog Log { get => log; private set => log = value; }

	public void LoadBattle(Encounter encounter)
	{
		enemies = EnemyScreen.context.LoadEncounter(encounter);
		turns = TurnQueue(enemies).GetEnumerator();
		UpdateGUI();
		StartCoroutine(ExecuteTurns());
	}

	IEnumerator ExecuteTurns()
	{
		yield return new WaitForEndOfFrame(); // wait to assign all enemy data like life and speed.
		while (turns.MoveNext()) // for each turn
		{
			if (turns.Current.GetType() == typeof(PlayerTurn))
			{
				playerData.haveTurn = true;
				ShowBattleMenu();
			}

			var turnEnum = turns.Current.Execute();

			while (turnEnum.MoveNext()) // wait till turn executed
			{
				yield return new WaitForEndOfFrame();
			}

			if (playerData.Health <= 0) // game over
			{
				Debug.LogError("Game Over");
				yield break;
			}
			else if (enemies.Where(e => e.CurrentHealth.Count > 0).Count() == 0) // you win
			{
				Debug.LogError("You win");
				yield break;
			}
			UpdateGUI();
			yield return new WaitForSeconds(1); // delay between turns
		}
	}

	private void ShowBattleMenu()
	{
		log.gameObject.SetActive(false);
		battleMenu.gameObject.SetActive(true);
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
			turns.Add(new EnemyTurn(enemy, this));
		}

		while (turns.Count > 0)
		{
			turns = turns.Where(e=> e.IsAlive()).OrderByDescending(e => e.GetSpeed()).ToList();
			foreach (var turn in turns)
			{
				if (turn.IsAlive()) yield return turn;
				else continue;
			}
		}
	}
}
