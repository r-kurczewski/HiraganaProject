using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiragana.Battle.UI
{
	public interface IAttackInput
	{ 
		IEnumerator ChooseEnemy();
		IEnumerator TypeHiragana();
		IEnumerator ConfirmAttack();
	}
}
