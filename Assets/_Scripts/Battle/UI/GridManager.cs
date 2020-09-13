using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.Battle.UI
{
	[ExecuteInEditMode]
	public class GridManager : MonoBehaviour
	{
		private GridLayoutGroup grid;
		[SerializeField] private RectTransform fillToRect;

		public Vector2Int gridSize;
		public Vector2Int spacing;
		public Vector2Int margin;

		void Awake()
		{
			grid = GetComponent<GridLayoutGroup>();
		}

		private void Update()
		{
			Refresh();
		}

		private void OnEnable()
		{
			Refresh();
		}

		public void Refresh()
		{
			float width = fillToRect.rect.width;
			width -= 2 * margin.x;
			width -= (gridSize.x - 1) * spacing.x;
			width /= gridSize.x;

			float height = fillToRect.rect.height;
			height -= 2 * margin.y;
			height -= (gridSize.y - 1) * spacing.y;
			height /= gridSize.y;

			float ratio = width / height;
			grid.cellSize = new Vector2(width, height);
			grid.spacing = new Vector2(spacing.x, spacing.y);
			grid.padding = new RectOffset(margin.x, margin.x, margin.y, margin.y);
		}
	}
}