using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public abstract class BaseEffect
	{
		public EngineContext Context { get; set; }
		
		public BaseEffect (EngineContext context)
		{
			Context = context;
		}
	}
}

