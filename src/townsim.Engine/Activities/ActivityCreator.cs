using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class ActivityCreator
	{
		public EngineSettings Settings { get;set; }

		public ActivityCreator (EngineSettings settings)
		{
			Settings = settings;
		}

		public BaseActivity CreateActivity(Person actor, Type activityType, NeedEntry needEntry)
		{
			var arguments = new object[] {
				actor,
				needEntry,
				Settings
			};

			var activity = (BaseActivity)Activator.CreateInstance(activityType, arguments);

			return activity;
		}
	}
}

