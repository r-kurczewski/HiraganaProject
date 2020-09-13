
namespace Hiragana.Battle
{
	public interface IBattleTarget
	{
		int Speed { get; set; }

		bool Alive { get; }

		string Name { get; }

		int TurnProgress { get; set; }

		bool SkipTurn { get; set; }

		void ApplyEffect(Effect effect);

		bool AddStatus(Status status);

		bool RemoveStatus<T>() where T: Status;

		bool HaveStatus<T>() where T : Status;

		void ExecuteStatuses();
	}
}
