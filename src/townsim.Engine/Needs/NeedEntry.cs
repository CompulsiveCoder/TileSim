using System;
using townsim.Engine.Needs;

namespace townsim.Engine
{
	public class NeedEntry
	{
		public NeedType Type { get; set; }

		public decimal Quantity { get; set; }

		public decimal Priority { get; set; }

		// TODO: Remove if not needed
		//public BaseNeed Need { get;set; }

		public NeedEntry (
			NeedType needType,
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

