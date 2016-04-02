using System;
using townsim.Entities;

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

