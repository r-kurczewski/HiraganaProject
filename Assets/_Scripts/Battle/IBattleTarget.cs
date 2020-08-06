
namespace Hiragana.Battle
{
	public interface IBattleTarget
	{
		bool ApplyEffect(Effect effect);

		bool AddStatus(Status status);

		bool RemoveStatus(Status status);

		bool HaveStatus(Status status);
	}
}
