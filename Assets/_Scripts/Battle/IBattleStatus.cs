namespace Hiragana.Battle
{
	public interface IBattleStatus
	{
		bool Apply();

		void Add(int i);

		void Remove();
	}
}