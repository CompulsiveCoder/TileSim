using System;
using tilesim.Engine.Entities;
using datamanager.Data;
using tilesim.Log;

namespace tilesim.Engine
{
	[Serializable]
	public class EngineContext
	{
		public GameEnvironment World { get; set; }

		public EngineProcess Engine { get;set; }

		public EngineClock Clock { get; set; }

		public EngineSettings Settings { get; set; }

		public DataManager Data { get;set; }

        public LogWriter Log;

		public EngineInfo Info;

        public ConsoleHelper Console { get; set; }

        public Person Player { get; set; }

		#region Constructors
		public EngineContext (EngineSettings settings, DataManager data)
		{			
			Data = data;

			Settings = settings;

            Console = new ConsoleHelper (Settings);

			Clock = new EngineClock (Settings, Console);

			Info = new EngineInfo (Clock.StartTime, Settings);

            Log = new LogWriter (Settings.EngineId, data.Client);

			World = new GameEnvironment (this);

			if (Settings.IsVerbose)
				Console.WriteDebugLine ("Constructing engine context");
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
				Console.WriteDebugLine ("Initializing engine context");

			if (Engine == null)
				throw new Exception ("No game engine process has been attached. Use the AttachProcess(engine) function before initializing.");

			//Engine.Initialize ();

            // TODO: Should this be reimplemented here?
            SaveInfo ();
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
            World.Populator.PopulateFromSettings ();

            // TODO: Is this the best place and approach to set the player?
            if (World.People.Length > 0) {
                Player = World.People [0];
                Player.Settings = Settings.PlayerSettings;
            }
		}

		#region Attach process
		public void AttachProcess(EngineProcess process)
		{
			Engine = process;
		}
		#endregion


        public void SaveInfo()
        {
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
			return new GameCreator (EngineSettings.Default).Create ();
		}

        public static EngineContext New(EngineSettings settings)
        {
            return new GameCreator (settings).Create ();
        }
	}
}

