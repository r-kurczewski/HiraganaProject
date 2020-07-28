using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hiragana.Battle.Effects;

namespace Hiragana.Battle
{
	public interface IBattleTarget
	{
		bool ApplyDamage(int value);

		bool AddStatus(IStatus status);

		void RemoveStatus(IStatus status);
	}
}
