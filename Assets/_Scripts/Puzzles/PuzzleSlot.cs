using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Hiragana.Puzzles
{
	public class PuzzleSlot : MonoBehaviour, IDropHandler
	{
		public int slotID;

		public bool Correct => slotID == (Item?.GetComponent<Puzzle>().puzzleNumber ?? -1);

		public GameObject Item
		{
			get
			{
				if (transform.childCount == 1)
				{
					return transform.GetChild(0).gameObject;
				}
				else return null;
			}
		}

		void Start()
		{
			slotID = transform.GetSiblingIndex() + 1;
			transform.name = "Slot " + slotID.ToString("d2");
		}

		public void OnDrop(PointerEventData eventData)
		{
			if (Puzzle.item)
			{
				Puzzle.item.transform.SetParent(transform);
				Puzzle.item.transform.localPosition = Vector3.zero;
				FindObjectOfType<PuzzleCreator>().CheckPuzzle();
			}
		}
	}
}
