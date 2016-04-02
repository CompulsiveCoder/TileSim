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
			//Info = new EngineInfo(
		}
		#endregion

		#region Start
		public void Start()
		{
			if (Settings.IsVerbose)
				Console.WriteLine ("Starting engine context");

			if (Engine == null)
				throw new Exception ("No game engine process has been attached. Use the AttachProcess(engine) function before calling start.");

			Engine.Start ();
		}
		#endregion

		public void RunCycles(int numberOfCycles)
		{
			Engine.RunCycles (numberOfCycles);
		}

		public void Populate()
		{
			Engine.Populate ();
		}

		#region Attach process
		public void AttachProcess(EngineProcess process)
		{
			// TODO: Should the process be passed in via the constructor?
			Engine = process;
		}
		#endregion

		public static EngineContext New()
		{
			return new GameCreator (EngineSettings.Default).Create ();
		}
	}
}

