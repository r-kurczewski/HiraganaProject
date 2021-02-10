using Hiragana.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Hiragana.Editors
{
	[CustomEditor(typeof(EnemyType))]
	public class EnemyEditor : Editor
	{

		public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
		{
			try
			{
				EnemyType enemy = (EnemyType)target;

				if (enemy == null || enemy.sprite == null)
					return null;

				// example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
				// Alpha8 or one of float formats
				Texture2D tex = new Texture2D(width, height);
				var spriteTexture = AssetPreview.GetAssetPreview(enemy.sprite);
				EditorUtility.CopySerialized(spriteTexture, tex);

				return tex;
			}
			catch { return null; }
		}
	}
}
