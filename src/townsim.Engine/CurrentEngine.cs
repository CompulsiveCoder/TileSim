using System;
using townsim.Data;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using townsim.Entities;
using datamanager.Data;

namespace townsim.Engine
{
	public static class CurrentEngine
	{
		static public string Id { get; set; }

		static public EngineInfo Info { get;set; }

		static public EngineClock Clock { get;set; }

		static public townsimEngine[] CurrentEngines = new townsimEngine[]{ };

		static public Thread[] EngineThreads;

    	static public string PlayerId { get; set; }

		static public bool IsStarted { get { return !String.IsNullOrEmpty (Id); } }

		static public void StartThread(string engineId)
		{
			Console.WriteLine ("Launching engine thread " + engineId);

			ThreadStart threadStart = delegate {
				var engine = new townsimEngine(engineId);

				engine.CreateTown();
				engine.Start();

				Attach(engine.Info);
			};

			var engineThread = new Thread(threadStart);

			engineThread.IsBackground = true;
			engineThread.Start();
		}

		static public void StartGame()
		{
			StartThread(Guid.NewGuid().ToString());
		}

		static public void Attach(string engineId)
		{
			Id = engineId;
			DataConfig.Prefix = "TownSim-" + engineId.ToString();
			Info = new DataManager ().Get<EngineInfo>(Id);
			Clock = new EngineClock (Info.StartTime, Info.Settings);
			PlayerId = Info.PlayerId;
		}

		static public void Attach(EngineInfo info)
		{
			Id = info.Id;
			DataConfig.Prefix = "TownSim-" + info.Id.ToString();
			Info = info;
			Clock = new EngineClock (Info.StartTime, Info.Settings);
      		PlayerId = info.PlayerId;
		}

		static public void Add(townsimEngine engine)
		{
			var list = new List<townsimEngine> (CurrentEngines);
			if (!list.Contains (engine))
				list.Add (engine);
			CurrentEngines = list.ToArray ();
		}
	}
}

