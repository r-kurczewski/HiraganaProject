using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Puzzle
{
	[SelectionBase]
	public class Puzzle : MonoBehaviour
	{
		private bool draging = false;

		private void Start()
		{

		}

		private void Update()
		{
			if (draging)
			{
				transform.position = GetMousePos();
			}
		}

		private void OnMouseDown()
		{
			draging = true;
		}

		private void OnMouseUp()
		{
			draging = false;
			transform.localPosition = SnapToGrid(transform.localPosition);
		}

		private Vector3 GetMousePos()
		{
			return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		private Vector3 SnapToGrid(Vector3 pos)
		{
			float newX = Mathf.Round(transform.position.x);
			float newY = Mathf.Round(transform.position.y);
			Debug.Log(pos + " snapped to " + new Vector2(newX, newY));
			return new Vector2(newX, newY);
		}
	}
}
