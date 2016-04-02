using System;
using townsim.Engine.Needs;

namespace townsim.Engine
{
	public class ActivityAttribute : Attribute
	{
		public NeedType[] Needs { get; set; }

		public ActivityAttribute (params NeedType[] needs)
		{
			Needs = needs;
		}
	}
}

