using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
	public class ActivityInfo
	{
		public Type ActivityType { get; set; }

        public ActionType ActionType { get; set; }

        public ItemType ItemType { get; set; }

		public ActivityInfo ()
		{
		}

		public ActivityInfo(Type activityType)
		{
			ActivityType = activityType;

			DetectDetailsFromAttribute (activityType);
		}

		public bool IsSuited(ActionType actionType, ItemType itemType)
		{
            return ActionType == actionType && ItemType == itemType;
		}

		public void DetectDetailsFromAttribute(Type activityType)
		{
			var attributes = activityType.GetCustomAttributes (typeof(ActivityAttribute), true);

			if (attributes.Length == 0)
				throw new Exception ("No ActivityAttribute on '" + activityType.Name + "' activity type.");

			var attribute = (ActivityAttribute)attributes [0];

            ActionType = attribute.ActionType;
            ItemType = attribute.ItemType;
		}
	}
}

