using System;
using townsim.Engine.Needs;

namespace townsim.Engine
{
	public class ActivityInfo
	{
		public Type ActivityType { get; set; }

		public NeedType[] Needs { get; set; }

		public ActivityInfo ()
		{
		}

		public ActivityInfo(Type activityType)
		{
			ActivityType = activityType;

			DetectNeedsFromAttribute (activityType);
		}

		public bool IsSuited(NeedType need)
		{
			return Array.IndexOf (Needs, need) > -1;
		}

		public void DetectNeedsFromAttribute(Type activityType)
		{
			var attributes = activityType.GetCustomAttributes (typeof(ActivityAttribute), true);

			if (attributes.Length == 0)
				throw new Exception ("No ActivityAttribute on '" + activityType.Name + "' activity type.");

			var attribute = (ActivityAttribute)attributes [0];

			Needs = attribute.Needs;
		}
	}
}

