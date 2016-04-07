using System;
using townsim.Data;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using townsim.Engine.Entities;
using datamanager.Data;

namespace townsim.Engine
{
	public static class CurrentEngine
	{
		static public string Id { get; set; }

		static public EngineInfo Info { get;set; }

		static public EngineClock Clock { get;set; }

		static public EngineProcess[] CurrentEngines = new EngineProcess[]{ };

		static public Thread[] EngineThreads;

		// TODO: Remove if not needed
    	//static public string PlayerId { get; set; }

		static public bool IsStarted { get { return !String.IsNullOrEmpty (Id); } }

		static public void StartThread(string engineId)
		{
			throw new NotImplementedException ();
			/*Console.WriteDebugLine ("Launching engine thread " + engineId);

			ThreadStart threadStart = delegate {
				var engine = new EngineProcess(engineId);

				engine.CreateTown();
				engine.Start();

				Attach(engine.Info);
			};

			var engineThread = new Thread(threadStart);

			engineThread.IsBackground = true;
			engineThread.Start();*/
		}

		static public void StartGame()
		{
			StartThread(Guid.NewGuid().ToString());
		}

		static public void Attach(string engineId)
		{
			throw new NotImplementedException ();
			/*Id = engineId;
			DataConfig.Prefix = "TownSim-" + engineId.ToString();
			Info = new DataManager ().Get<EngineInfo>(Id);
			Clock = new EngineClock (Context);*/
		}

		static public void Attach(EngineInfo info)
		{

			throw new NotImplementedException ();
			/*
			Id = info.Id;
			DataConfig.Prefix = "TownSim-" + info.Id.ToString();
			Info = info;
			Clock = new EngineClock (Info.StartTime, Info.Settings);*/
		}

		static public void Add(EngineProcess engine)
		{
			var list = new List<EngineProcess> (CurrentEngines);
			if (!list.Contains (engine))
				list.Add (engine);
			CurrentEngines = list.ToArray ();
		}
	}
}

