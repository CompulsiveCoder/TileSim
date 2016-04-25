using System;
using tilesim.Engine.Entities;
using datamanager.Data;

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

			var process = CreateProcess (context);

			// Attach the process to the context
			context.AttachProcess(process);

			return context;
		}

		public EngineProcess CreateProcess(EngineContext context)
		{
			var process = new EngineProcess (context);

			return process;
		}

		public DataManager CreateDataManager()
		{
			var dataManager = new DataManager();

			dataManager.IsVerbose = Settings.IsVerbose;

			return dataManager;
		}
	}
}

