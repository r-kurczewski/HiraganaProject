
namespace Hiragana.Battle
{
	public interface IBattleTarget
	{
		int Speed { get; set; }

		bool Alive { get; }

		string Name { get; }

		bool SkipTurn { get; set; }

		void ApplyEffect(Effect effect);

		bool AddStatus(Status status);

		bool RemoveStatus(Status status);

		bool HaveStatus(Status status);

		void ExecuteStatuses();

	}
}
