using System.Collections;

namespace Hiragana.Battle
{
	public interface ITurn
	{
		IBattleTarget Target { get; }

		IEnumerator Execute();

	}
}
