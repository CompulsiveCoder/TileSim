using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
	public class ActivityAttribute : Attribute
    {
        public ActionType ActionType { get; set; }
        public ItemType ItemType { get; set; }
        public PersonVitalType VitalType { get; set; }

        public ActivityAttribute (ActionType actionType, ItemType itemType, PersonVitalType vitalType)
		{
            ActionType = actionType;
            ItemType = itemType;
            VitalType = vitalType;
		}
	}
}

