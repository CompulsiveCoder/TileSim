using System;
using townsim.Entities;

namespace townsim.Engine.Activities
{
	[Serializable]
	public abstract class BaseActivity
	{
		public EngineSettings Settings { get; set; }

		public EngineClock Clock { get; set; }

		public Person Person { get; set; }

		public BaseActivity ()
		{
		}

		public BaseActivity (EngineSettings settings)
		{
			Settings = settings;
		}

		public BaseActivity (EngineSettings settings, EngineClock clock)
		{
			Settings = settings;
			Clock = clock;
		}

		public abstract void Act();

		public void Start(Person person)
		{
			person.Activity = this;
			Person = person;
		}
	}
}

