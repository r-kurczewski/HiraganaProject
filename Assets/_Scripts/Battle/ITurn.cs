using System.Collections;

namespace Hiragana.Battle
{
	public interface ITurn
	{
		IEnumerator Execute();

		int GetSpeed();

		bool IsAlive();

		string GetName();
	}
}
