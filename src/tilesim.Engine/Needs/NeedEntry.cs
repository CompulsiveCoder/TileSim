using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
    [Serializable]
	public class NeedEntry
	{
        public ActionType ActionType { get;set; }

        public PersonVitalType VitalType { get;set; }

		public ItemType ItemType { get; set; }

		public decimal Quantity { get; set; }

		public decimal Priority { get; set; }

		public NeedEntry (
            ActionType actionType,
            ItemType itemType,
            PersonVitalType vitalType,
			decimal quantity,
			decimal priority
		)
		{
            ActionType = actionType;
			ItemType = itemType;
            VitalType = vitalType;
			Quantity = quantity;
			Priority = priority;
		}
	}
}

