using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public class NeedEntry
	{
		public ItemType Type { get; set; }

		public decimal Quantity { get; set; }

		public decimal Priority { get; set; }

		// TODO: Remove if not needed
		//public BaseNeed Need { get;set; }

		public NeedEntry (
			ItemType needType,
			decimal quantity,
			decimal priority//,
			//BaseNeed need
		)
		{
			Type = needType;
			Quantity = quantity;
			Priority = priority;
			//Need = need;
		}
	}
}

