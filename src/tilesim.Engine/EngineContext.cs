using System;
using tilesim.Engine.Entities;
using datamanager.Data;
using tilesim.Log;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace tilesim.Engine
{
    [Serializable]
    [JsonObject("EngineContext", IsReference=true)]
	public class EngineContext
	{
		public GameEnvironment World { get; set; }

        [NonSerialized]
        public EngineProcess Engine;

		public EngineClock Clock { get; set; }

		public EngineSettings Settings { get; set; }

		public DataManager Data { get;set; }

        public LogWriter Log;

		public EngineInfo Info;

        public ConsoleHelper Console { get; set; }

        public Person Player { get; set; }

        public Queue<BaseOrder> Orders = new Queue<BaseOrder>();

		#region Constructors
		public EngineContext (EngineSettings settings, DataManager data)
		{			
			Data = data;

			Settings = settings;

            Console = new ConsoleHelper (Settings);

            Console.WriteDebugLine ("    Constructing engine context");

			Clock = new EngineClock (Settings, Console);

			Info = new EngineInfo (Clock.StartTime, Settings);

            Log = new LogWriter (Settings.EngineId, data.Provider);

			World = new GameEnvironment (this);

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
		    Console.WriteDebugLine ("Initializing engine context");

			if (Engine == null)
				throw new Exception ("No game engine process has been attached. Use the AttachProcess(engine) function before initializing.");

            // TODO: Does anything else need to happen here? If not should this function be removed?
		}
		#endregion

        public void AddCompleteLogic ()
        {
            var logic = GameLogic.NewComplete (Settings, Console);

            World.Logic = logic;
        }

        public void Run()
        {
            Engine.Run ();
        }

		public void Run(int numberOfCycles)
		{
			Engine.Run (numberOfCycles);
		}

		public void PopulateFromSettings()
		{
            Console.WriteDebugLine ("    Populating game engine from settings");

            World.Populator.PopulateFromSettings ();

            // TODO: Is this the best place and approach to set the player?
            if (World.People.Length > 0) {
                Player = World.People [0];

                Console.WriteDebugLine ("      Selecting player: " + Player.Id);
                Player.Settings = Settings.PlayerSettings;
            }
		}

		#region Attach process
		public void AttachProcess(EngineProcess process)
        {
            process.Context.Console.WriteDebugLine ("   Attaching game engine process to context");

			Engine = process;
		}
		#endregion


        public void SaveInfo()
        {
            Console.WriteDebugLine ("  Saving game engine info");

            // IMPORTANT: The ID of the engine must be set to the ID of the engine info
            Info.Id = Settings.EngineId;

            Data.Save (Info);

            Data.Save (World.People);

            // TODO: Remove if not needed
            /*foreach (var tile in Context.World.Tiles)
            {
                if (!Context.Data.Exists(tile))
                    Context.Data.Save(tile);

                Context.Data.Save (tile.Buildings);
            }*/
        }


		public static EngineContext New()
		{
            return new GameContextCreator (EngineSettings.Default, new DataManager()).Create ();
		}

        public static EngineContext New(EngineSettings settings, DataManager data)
        {
            return new GameContextCreator (settings, data).Create ();
        }

        public void AddOrder(BaseOrder order)
        {
            Orders.Enqueue (order);
            //Data.Save (order);
        }
	}
}

