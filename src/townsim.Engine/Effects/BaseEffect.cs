using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public abstract class BaseEffect
	{
        public EngineSettings Settings { get; set; }
		
        public BaseEffect (EngineSettings settings)
		{
            Settings = settings;
		}

        public void Apply ()
        {
            Execute ();

            Finished ();
        }

        public abstract void Execute();

        public abstract void Finished ();
	}
}

