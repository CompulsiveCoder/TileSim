using System;
using tilesim.Engine.Entities;
using Newtonsoft.Json;

namespace tilesim.Engine
{
	public abstract class BaseDecision
	{
		//[JsonIgnore]
		//[NonSerialized]
		//public EngineContext Context;

		[JsonIgnore]
		[NonSerialized]
		public EngineSettings Settings;

		// TODO: Clean up
//		public BaseDecision (EngineContext context)

		public BaseDecision (EngineSettings settings)
		{
			Settings = settings;
		}

		public abstract void Decide(Person person);
	}
}

