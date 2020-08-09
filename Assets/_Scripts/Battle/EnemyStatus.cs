
namespace Hiragana.Battle
{
	public interface EnemyStatus
	{
		bool Keep { get; set; }

		void Execute(IBattleTarget target);

		void Merge(Status newStatus);
	}
}