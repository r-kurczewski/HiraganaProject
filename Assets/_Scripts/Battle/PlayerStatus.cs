
namespace Hiragana.Battle
{
	public interface PlayerStatus
	{
		bool Keep { get; set; }

		void Execute(IBattleTarget target);

		void Merge(Status newStatus);

		void OnRemove();
	}
}