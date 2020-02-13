using Hiragana.Battle;
using Hiragana.Battle.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (EventSystem.current.currentSelectedGameObject.GetComponent<Enemy>())
			{
				FocusInterface();
			}
			else
			{
				FocusEnemy();
			}
		}
	}

	public void FocusEnemy()
	{
		FindObjectOfType<EnemyList>().getEnemy(0).Select();
	}

	public void FocusInterface()
	{
		FindObjectOfType<MenuOption>().firstSelection.Select();
	}

}
