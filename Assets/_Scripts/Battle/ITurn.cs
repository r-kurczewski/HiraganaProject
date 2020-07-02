
using System.Collections;

namespace Hiragana.Battle
{
	public interface ITurn
	{
		void Execute();

		int GetSpeed();

		bool IsAlive();
	}
}
