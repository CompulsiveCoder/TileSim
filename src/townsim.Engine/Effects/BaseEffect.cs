using System;
using townsim.Engine.Entities;

namespace townsim.Engine
{
	public abstract class BaseEffect
	{
        public EngineSettings Settings { get; set; }
		
        public ConsoleHelper Console { get;set; }

        public BaseEffect (EngineSettings settings, ConsoleHelper console)
		{
            Settings = settings;
            Console = console;
		}

        public abstract bool IsApplicable();

        public void Apply ()
        {
            if (IsApplicable ()) {
                Execute ();

                Finished ();
            }
        }

        public abstract void Execute();

        public abstract void Finished ();
	}
}

