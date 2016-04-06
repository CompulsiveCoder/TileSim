using System;
using townsim.Engine.Entities;
using townsim.Data;
using datamanager.Data;

namespace townsim.Engine
{
	[Serializable]
	public class EngineContext
	{
		public GameEnvironment World { get; set; }

		public EngineProcess Engine { get;set; }

		// TODO: Remove if not needed
		//public LogWriter PlayerLog { get;set; }

		public EngineClock Clock { get; set; }

		public EngineSettings Settings { get; set; }

		public DataManager Data { get;set; }

		public LogWriter Log;

		public EngineInfo Info;

		#region Constructors
		public EngineContext (EngineSettings settings, DataManager data)
		{			
			Data = data;

			Settings = settings;

			Clock = new EngineClock (Settings);

			Info = new EngineInfo (Clock.StartTime, Settings);

            Log = new LogWriter (Settings.EngineId, data.Client);

			World = new GameEnvironment (this);

			if (Settings.IsVerbose)
				Console.WriteLine ("Constructing engine context");
		}

		public EngineContext (EngineProcess engine)
		{
			throw new NotImplementedException ();
			//Engine = engine;

			//Construct ();
		}

		public void Construct()
		{
		}
		#endregion

		#region Start
		public void Initialize()
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("Initializing engine context");

			if (Engine == null)
				throw new Exception ("No game engine process has been attached. Use the AttachProcess(engine) function before initializing.");

			Engine.Initialize ();
		}
		#endregion

        public void InitializeCompleteLogic ()
        {
            var logic = GameLogic.NewComplete (Settings);

            World.Logic = logic;
        }

		public void Run(int numberOfCycles)
		{
			Engine.Run (numberOfCycles);
		}

		public void PopulateFromSettings()
		{
            World.Populator.PopulateFromSettings ();
		}

		#region Attach process
		public void AttachProcess(EngineProcess process)
		{
			Engine = process;
		}
		#endregion

		public static EngineContext New()
		{
			return new GameCreator (EngineSettings.Default).Create ();
		}

        public static EngineContext New(EngineSettings settings)
        {
            return new GameCreator (settings).Create ();
        }
	}
}

