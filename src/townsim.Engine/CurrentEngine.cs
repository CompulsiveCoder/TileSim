using System;
using townsim.Data;
using System.Web;
using System.Collections.Generic;
using System.Threading;

namespace townsim.Engine
{
	public static class CurrentEngine
	{
		static public string Id { get; set; }

		static public townsimEngine[] CurrentEngines = new townsimEngine[]{ };

		static public Thread[] EngineThreads;

		static public void StartThread(string engineId)
		{
			Console.WriteLine ("Launching engine thread " + engineId);

			System.Threading.ThreadStart threadStart = delegate {
				var engine = new townsimEngine(engineId);
				engine.CreateTown();
				engine.Start();
			};
			var engineThread = new System.Threading.Thread(threadStart);
			engineThread.IsBackground = true;
			engineThread.Start();

			Attach (engineId);
		}

		static public void StartGame()
		{
			StartThread("");
		}

		static public void Attach(string engineId)
		{
			Id = engineId;
			DataConfig.Prefix = "TownSim-" + engineId;
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

