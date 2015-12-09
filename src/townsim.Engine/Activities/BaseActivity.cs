using System;
using townsim.Entities;

namespace townsim.Engine
{
	public abstract class BaseActivity
	{
		public EngineSettings Settings { get; set; }

		public BaseActivity (EngineSettings settings)
		{
			Settings = settings;
		}
	}
}

