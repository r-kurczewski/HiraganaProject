using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiragana.Other
{
	public abstract class SaveObject : MonoBehaviour
	{ 
		public abstract void Save();

		public abstract void Load();
	}
}