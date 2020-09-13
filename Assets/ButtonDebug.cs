using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana
{
	public class ButtonDebug : MonoBehaviour
	{
		public Button first;
		public Button second;

		void Start()
		{
			StartCoroutine(Test());
		}

		void Update()
		{

		}

		IEnumerator Test()
		{
			yield return new WaitForSeconds(1);
			first.Select();
			second.Select();
		}
	}
}