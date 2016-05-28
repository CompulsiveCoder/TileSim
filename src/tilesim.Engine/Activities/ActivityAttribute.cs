using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
	public class ActivityAttribute : Attribute
    {
        public ActivityVerb Verb { get; set; }
        public ItemType ItemType { get; set; }
        public PersonVitalType VitalType { get; set; }

        public ActivityAttribute (ActivityVerb verb)
        {
            Verb = verb;
        }

        public ActivityAttribute (ActivityVerb actionType, ItemType itemType, PersonVitalType vitalType)
		{
            Verb = actionType;
            ItemType = itemType;
            VitalType = vitalType;
		}
	}
}

