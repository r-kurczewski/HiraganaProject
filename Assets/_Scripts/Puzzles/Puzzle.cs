using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Hiragana.Puzzles
{
	public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public static GameObject item;
		public static Transform startParent;
		Vector3 startPosition;
		public bool draggable = true;
		public int puzzleNumber;

		public void OnBeginDrag(PointerEventData eventData)
		{

			item = gameObject;
			startPosition = transform.position;
			startParent = transform.parent;
			transform.SetParent(GameObject.Find("Canvas").transform);
			item.GetComponent<CanvasGroup>().blocksRaycasts = false;

		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.position = ClampedMousePos(Input.mousePosition);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			item = null;
			GetComponent<CanvasGroup>().blocksRaycasts = true;

		}

		public static Vector2 ClampedMousePos(Vector2 mousePos)
		{
			var result = mousePos;
			result.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
			result.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
			return result;
		}
	}
}

