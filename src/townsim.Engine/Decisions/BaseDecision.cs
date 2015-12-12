using System;
using townsim.Entities;

namespace townsim.Engine
{
	public abstract class BaseDecision
	{
		public EngineSettings Settings { get; set; }

		public BaseDecision (EngineSettings settings)
		{
			Settings = settings;
		}

		public abstract ActivityType Decide(Person person);
	}
}

