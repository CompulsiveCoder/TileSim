using System;
using tilesim.Engine.Entities;
using datamanager.Data;
using datamanager.Data.Providers.Redis;

namespace tilesim.Engine
{
	public class GameContextCreator
	{
		public EngineSettings Settings { get;set; }

        public DataManager Data { get;set; }

        public GameContextCreator (EngineSettings settings, DataManager data)
		{
			Settings = settings;
            Data = data;
		}

		public EngineContext Create()
		{
			var context = new EngineContext (Settings, Data);

            context.Console.WriteDebugLine ("    Creating game engine context");

			var process = CreateProcess (context);

			// Attach the process to the context
			context.AttachProcess(process);

			return context;
		}

		public EngineProcess CreateProcess(EngineContext context)
        {
            context.Console.WriteDebugLine ("    Creating game engine process");

			var process = new EngineProcess (context);

			return process;
		}
	}
}

