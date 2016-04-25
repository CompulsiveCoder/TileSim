using System;
using tilesim.Data;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using tilesim.Engine.Entities;
using datamanager.Data;

namespace tilesim.Engine
{
	public static class CurrentEngine
	{
		/*static public string Id { get; set; }

		static public EngineInfo Info { get;set; }

		static public EngineClock Clock { get;set; }

		static public EngineProcess Process = new EngineProcess{ };
*/

        static public EngineContext Context { get;set; }

		static public Thread EngineThread;

		// TODO: Remove if not needed
    	//static public string PlayerId { get; set; }

        static public bool IsStarted { get { return Context != null; } }

		static public void StartThread(string engineId)
		{
            if (engineId == String.Empty)
                engineId = Guid.NewGuid ().ToString ();

			Console.WriteLine ("Launching engine thread " + engineId);

            var context = EngineContext.New();
            context.PopulateFromSettings();
            context.AddCompleteLogic();

            context.Initialize();

            Attach(context);

			ThreadStart threadStart = delegate {

                context.Run();

                // TODO: Remove if not needed
				/*var engine = new EngineProcess(engineId);

				engine.CreateTile();
				engine.Start();*/
                
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
			/*Id = engineId;
			DataConfig.Prefix = "TileSim-" + engineId.ToString();
			Info = new DataManager ().Get<EngineInfo>(Id);
			Clock = new EngineClock (Context);*/
		}

        static public void Attach(EngineContext context)
		{
            Context = context;
		}
	}
}

