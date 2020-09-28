using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.World
{
	public class Puzzle : MonoBehaviour
	{
		public Romaji romaji;
		bool trigger;

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Return) && trigger)
			{

			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			trigger = true;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			trigger = false;
		}
	}
}