using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hiragana.Battle.UI
{
	public class StandardMenu : MenuOption
	{
		private void OnEnable()
		{
			StartCoroutine(SelectButtton());
		}

		public IEnumerator SelectButtton()
		{
			yield return new WaitUntil(() => firstSelection?.isActiveAndEnabled ?? false);
			firstSelection.Select();
		}
	}
}
