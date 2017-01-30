using System;
using tilesim.Engine.Entities;
using datamanager.Data;
using datamanager.Data.Providers.Redis;

namespace tilesim.Engine
{
	public class GameCreator
	{
		public EngineSettings Settings { get;set; }

		public GameCreator (EngineSettings settings)
		{
			Settings = settings;
		}

		public EngineContext Create()
		{
			// Create the required objects
			var data = CreateDataManager ();

			var context = new EngineContext (Settings, data);

            context.Console.WriteDebugLine ("Creating game engine context");

			var process = CreateProcess (context);

			// Attach the process to the context
			context.AttachProcess(process);

			return context;
		}

		public EngineProcess CreateProcess(EngineContext context)
        {
            context.Console.WriteDebugLine ("Creating game engine process");

			var process = new EngineProcess (context);

			return process;
		}

		public DataManager CreateDataManager()
		{
            var provider = new RedisDataProvider ();

            var dataManager = new DataManager(provider);

            dataManager.Settings.IsVerbose = Settings.IsVerbose;

			return dataManager;
		}
	}
}

