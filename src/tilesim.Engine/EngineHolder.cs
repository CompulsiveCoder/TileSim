using System;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using tilesim.Engine.Entities;
using datamanager.Data;

namespace tilesim.Engine
{
	public static class EngineHolder
	{
        static public EngineContext Context { get;set; }

		static public Thread EngineThread;

        static public bool IsStarted { get { return Context != null; } }

        static public void StartThread(string engineId, int gameSpeed)
		{
            if (engineId == String.Empty)
                engineId = Guid.NewGuid ().ToString ();

			Console.WriteLine ("Launching engine thread " + engineId);

            var context = EngineContext.New();
            context.Settings.GameSpeed = gameSpeed;
            context.PopulateFromSettings();
            context.AddCompleteLogic();

            context.Initialize();

            Attach(context);

			ThreadStart threadStart = delegate {
                context.Run();                
			};

			var engineThread = new Thread(threadStart);

			engineThread.IsBackground = true;
			engineThread.Start();

		}

        static public void StartGame(int gameSpeed)
		{
			StartThread(Guid.NewGuid().ToString(), gameSpeed);
		}

		static public void Attach(string engineId)
		{
            throw new NotImplementedException ();
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

