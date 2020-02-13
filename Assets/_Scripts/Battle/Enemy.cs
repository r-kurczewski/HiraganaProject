using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle
{
	[SelectionBase]
	public class Enemy : MonoBehaviour
	{
		void Start()
		{

		}

		void Update()
		{

		}

		public void Select()
		{
			GetComponent<Selectable>().Select();
		}
	}
}
