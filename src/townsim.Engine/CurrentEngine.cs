using System;
using townsim.Data;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using townsim.Entities;

namespace townsim.Engine
{
	public static class CurrentEngine
	{
		static public Guid Id { get; set; }

		static public EngineInfo Info { get;set; }

		static public EngineClock Clock { get;set; }

		static public townsimEngine[] CurrentEngines = new townsimEngine[]{ };

		static public Thread[] EngineThreads;

    	static public Guid PlayerId { get; set; }

		static public void StartThread(Guid engineId)
		{
			Console.WriteLine ("Launching engine thread " + engineId);

			var startTime = DateTime.MinValue;
			EngineSettings settings = null;

			System.Threading.ThreadStart threadStart = delegate {
				var engine = new townsimEngine(engineId);
				engine.CreateTown();
				engine.Start();
				startTime = engine.Clock.StartTime;
				settings = engine.Settings;
        		Attach( new EngineInfo (engineId, startTime, settings, engine.Player.Id));

			};
			var engineThread = new System.Threading.Thread(threadStart);
			engineThread.IsBackground = true;
			engineThread.Start();


		}

		static public void StartGame()
		{
			StartThread(Guid.NewGuid());
		}

		static public void Attach(Guid engineId)
		{
			Id = engineId;
			DataConfig.Prefix = "TownSim-" + engineId.ToString();
			Info = new EngineInfoReader ().Read (Id);
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

