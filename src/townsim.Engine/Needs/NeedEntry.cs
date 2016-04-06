using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public class NeedEntry
	{
        public ActionType ActionType { get;set; }

		public ItemType ItemType { get; set; }

		public decimal Quantity { get; set; }

		public decimal Priority { get; set; }

		public NeedEntry (
            ActionType actionType,
			ItemType needType,
			decimal quantity,
			decimal priority
		)
		{
            ActionType = actionType;
			ItemType = needType;
			Quantity = quantity;
			Priority = priority;
		}
	}
}

