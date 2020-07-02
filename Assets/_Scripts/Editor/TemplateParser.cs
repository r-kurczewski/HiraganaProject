using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hiragana.Editors
{
	public class TemplateParser : UnityEditor.AssetModificationProcessor
	{

		public static void OnWillCreateAsset(string path)
		{
			path = path.Replace(".meta", "");
			int index = path.LastIndexOf(".");
			string file = path.Substring(index);
			if (file != ".cs" && file != ".js" && file != ".boo") return;
			index = Application.dataPath.LastIndexOf("Assets");
			path = Application.dataPath.Substring(0, index) + path;
			file = System.IO.File.ReadAllText(path);

			var pathStrings = path.Split(new char[] { '/' }).ToList();
			string namespaceString;

			if (pathStrings.Contains("Editor"))
			{
				namespaceString = Application.productName + ".Editors";
			}
			else
			{

				List<string> namespaceStrings = new List<string>() { Application.productName };
				for (int i = 5; i < pathStrings.Count - 1; i++)
					namespaceStrings.Add(pathStrings[i]);
				namespaceString = string.Join(".", namespaceStrings);
			}
			file = file.Replace("#PROJECTNAME#", namespaceString);

			System.IO.File.WriteAllText(path, file);
			AssetDatabase.Refresh();
		}
	}
}