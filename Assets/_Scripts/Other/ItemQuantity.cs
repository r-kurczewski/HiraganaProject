using System;

namespace Hiragana.Other
{
		[Serializable]
		public class ItemQuantity<T>
		{
			public T item;
			public uint quantity = 1;

			public ItemQuantity() { }

			public ItemQuantity(T item, uint quantity)
			{
				this.item = item;
				this.quantity = quantity;
			}
		}
}
