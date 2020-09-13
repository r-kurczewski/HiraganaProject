using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	public class ButtonFocus : MonoBehaviour
	{
		Selectable lastSelected;

		private void Update()
		{
			if (!EventSystem.current.currentSelectedGameObject)
			{
				if (lastSelected)
				{
					lastSelected.Select();
				}
				else
				{
					EventSystem.current.firstSelectedGameObject?.GetComponent<Selectable>().Select();
				}
			}
			else
			{
				lastSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
			}
		}
	}
}
