using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hiragana.World
{
	[ExecuteInEditMode]
	public class SnapToGrid : MonoBehaviour
	{
		private void Snap()
		{
			var x = transform.position.x;
			var offset = 0.5f;
			var roundTo = 1;
			var y = transform.position.y;
			x = Mathf.RoundToInt((x - offset) / roundTo) * roundTo + offset;
			y = Mathf.RoundToInt(y - offset / roundTo) * roundTo + offset;
			transform.position = new Vector3(x, y, transform.position.z);
		}

		void Update()
		{
			#if UNITY_EDITOR
			Snap();
			#endif
		}
	}
}