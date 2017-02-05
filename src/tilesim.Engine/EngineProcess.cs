using System;
using System.Threading;
using tilesim.Engine.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using datamanager.Data;
using tilesim.Engine.Activities;
using Newtonsoft.Json;

namespace tilesim.Engine
{
	public class EngineProcess : IComponent
	{
		public DateTime GameStartTime = DateTime.MinValue;

		public bool EnableDatabase = true;

		public EngineContext Context;

		public bool IsRunning;

		public PersonEngine Persons { get; set; }

        public GameConsoleSummarizer Summarizer { get; set; }

        public OrderProcessor OrderProcessor;

        public int CycleNumber = 0;

		public EngineProcess (EngineContext context)
		{
			Construct (context);
		}

		public void Construct(EngineContext context)
		{			
			Context = context;

			if (context.Settings.IsVerbose)
				context.Console.WriteDebugLine ("    Constructing game engine process");

			Persons = new PersonEngine (context);

            if (String.IsNullOrEmpty(Context.Settings.EngineId)){
                Context.Settings.EngineId = Guid.NewGuid ().ToString();
			}

			// TODO: The DataConfig.Prefix static singleton should be moved to a normal object property
			Context.Data.Settings.Prefix = "TileSim-" + Context.Settings.EngineId;

            Summarizer = new GameConsoleSummarizer (Context);

            OrderProcessor = new OrderProcessor (Context, Context.Data);
		}

		public void Run()
		{
            IsRunning = true;

            while (IsRunning)
                Run (1);
		}

		public void SleepUntilNextCycle(DateTime cycleStartTime)
		{
			var cycleCompleteTime = DateTime.Now;

			var cycleDuration = cycleCompleteTime.Subtract (cycleStartTime);

			Context.Console.WriteDebugLine ("Duration: " + cycleDuration.Milliseconds + " milliseconds (max " + Context.Settings.CycleDuration + ")");

			var sleepDurationInMilliseconds = Context.Settings.CycleDuration - cycleDuration.Milliseconds;
			if (sleepDurationInMilliseconds > 0)
				Thread.Sleep (sleepDurationInMilliseconds);
		}

		public void Run(int numberOfCycles)
		{
			if (Context.Settings.IsVerbose)
				Context.Console.WriteDebugLine ("Running engine for " + numberOfCycles + " cycles.");

			for (int i = 0; i < numberOfCycles; i++)
			{
				RunCycle ();

                // TODO: Is this IsAlive check needed? It's checked during RunCycle via EnsurePlayerIsAlive function
                if (!Context.Player.IsAlive)
                    PlayerDied ();
                else {
                    Summarizer.WriteSummary ();
                }
			}
		}

		public void RunCycle()
		{
            var cycleStartTime = DateTime.Now;

            Context.Console.WriteDebugLine ("");
            Context.Console.WriteDebugLine ("Starting game engine cycle... #" + CycleNumber);
            Context.Console.WriteDebugLine ("");

            for (int i = 0; i < Context.Settings.GameSpeed; i++) {
                
                Context.Console.WriteDebugLine ("");
                Context.Console.WriteDebugLine ("Starting game engine sub-cycle... #" + CycleNumber + "." + (i+1));
                Context.Console.WriteDebugLine ("");

                // Environment
                ProcessOrders ();

                ProcessEffects ();

                // People
                ProcessPeople ();

                EnsurePlayerIsAlive ();
            }

            Context.Console.WriteDebugLine ("");
            Context.Console.WriteDebugLine ("Completed game engine cycle");
            Context.Console.WriteDebugLine ("");

            SleepUntilNextCycle(cycleStartTime);

            CycleNumber++;
		}

        public void ProcessOrders()
        {
            OrderProcessor.ProcessAll ();
        }
           

        public void ProcessEffects()
        {
            foreach (var effect in Context.World.Logic.Effects) {
                if (effect is BasePersonEffect) {
                    foreach (var person in Context.World.People) {
                        ((BasePersonEffect)effect).Apply (person);
                    }
                }
                else
                    effect.Apply ();
            }
        }

        public void ProcessPeople()
        {
            foreach (var person in Context.World.People) {
                RunCycleForPerson (person);
            }
        }

        public void EnsurePlayerIsAlive()
        {
            if (Context.Player == null)
                throw new Exception ("Context.Player == null");
            if (!Context.Player.IsAlive)
                PlayerDied ();
        }


		public void RunCycleForPerson(Person person)
		{
			Persons.StartSingleCycle (person);


			// TODO: Ensure all the following activities have been reimplemented then remove this code
			/*var decideActivity = new DecideActivity ();
			var collectWaterActivity = new CollectWaterActivity (Settings);
			var drinkActivity = new DrinkActivity (Settings);
			var eatActivity = new EatActivity (Settings);
			var buildActivity = new BuildActivity (Settings, Clock);
			var harvestActivity = new HarvestActivity (Settings, Clock);
			var gardenActivity = new GardenActivity (Settings, Clock);
			var plantTreesActivity = new PlantTreesActivity (Settings, Clock);

			// Decision-making
			decideActivity.Act (person);

			// Activities
			collectWaterActivity.Act(person);
			drinkActivity.Act(person);
			eatActivity.Act(person);
			buildActivity.Act (person);

			gardenActivity.Act (person);
			plantTreesActivity.Act (person);
			*/
		}

		public void PlayerDied()
		{			
			Context.Console.WriteDebugLine ("The player died.");
            Context.Console.WriteDebugLine ("Game Over");
		}

		public void Dispose()
		{
			Context.Data.Delete(Context.Info);
		}

		public event EventHandler Disposed;

        [JsonIgnore]
		public ISite Site {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}
	}
}

