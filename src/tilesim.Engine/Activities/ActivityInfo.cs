using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
	public class ActivityInfo
	{
		public Type ActivityType { get; set; }

        public ActionType ActionType { get; set; }

        public ItemType ItemType { get; set; }

        public PersonVitalType VitalType { get; set; }

		public ActivityInfo ()
		{
		}

		public ActivityInfo(Type activityType)
		{
			ActivityType = activityType;

			DetectDetailsFromAttribute (activityType);
		}

        public bool IsSuited(ActionType actionType, ItemType itemType, PersonVitalType vitalType)
		{
            return ActionType == actionType
                && (ItemType == itemType
                    || VitalType == vitalType);
		}

		public void DetectDetailsFromAttribute(Type activityType)
		{
			var attributes = activityType.GetCustomAttributes (typeof(ActivityAttribute), true);

			if (attributes.Length == 0)
				throw new Exception ("No ActivityAttribute on '" + activityType.Name + "' activity type.");

			var attribute = (ActivityAttribute)attributes [0];

            ActionType = attribute.ActionType;
            ItemType = attribute.ItemType;
            VitalType = attribute.VitalType;
		}

        public override string ToString ()
        {
            return string.Format ("[{0}, {1} {2} ({3})]", ActivityType, ActionType, ItemType, VitalType);
        }
	}
}

