using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
	public abstract class BasePopulator
	{
		public EngineContext Context { get;set; }

		public BaseGameEntity Target { get; set; }

		public BasePopulator (EngineContext context)
		{
			Context = context;
		}

		public abstract void Populate();
	}
}

