using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public class ActivityAttribute : Attribute
	{
		public ItemType[] Needs { get; set; }

		public ActivityAttribute (params ItemType[] needs)
		{
			Needs = needs;
		}
	}
}

