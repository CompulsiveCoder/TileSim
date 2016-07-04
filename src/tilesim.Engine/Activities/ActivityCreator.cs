using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
	public class ActivityCreator
	{
		public EngineSettings Settings { get;set; }

        public ConsoleHelper Console { get;set; }

        public ActivityCreator (EngineSettings settings, ConsoleHelper console)
		{
			Settings = settings;
            Console = console;
		}

		public BaseActivity CreateActivity(Person actor, Type activityType, NeedEntry needEntry)
		{
			var arguments = new object[] {
				actor,
				needEntry,
				Settings,
                Console
			};

			var activity = (BaseActivity)Activator.CreateInstance(activityType, arguments);

			return activity;
		}
	}
}

