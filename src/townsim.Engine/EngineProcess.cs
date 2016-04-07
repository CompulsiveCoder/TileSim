using System;
using townsim.Data;
using System.Threading;
using townsim.Engine.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using datamanager.Data;
using townsim.Engine.Activities;

namespace townsim.Engine
{
	public class EngineProcess : IComponent
	{
		public DateTime GameStartTime = DateTime.MinValue;

		public bool EnableDatabase = true;

		public EngineContext Context;

		public bool IsRunning;

		//public EffectEngine Effects { get; set; }

		public PersonEngine Persons { get; set; }

        public GameConsoleSummarizer Summarizer { get; set; }

		/*public EngineProcess ()
		{
			Initialize ();

			Attach ();
		}

		public EngineProcess (Person person, Town town)
		{
			Player = person;

			AddTown(town);

			Initialize ();

			Attach ();
		}*/

		public EngineProcess (EngineContext context)
		{
			Construct (context);
		}

		public void Construct()
		{
			throw new NotImplementedException ();
		}

		public void Construct(EngineContext context)
		{			
			Context = context;

			if (context.Settings.IsVerbose)
				context.Console.WriteDebugLine ("Constructing game engine process");

			Persons = new PersonEngine (context);

			// TODO: Check if needed
			/*if (String.IsNullOrEmpty(Id)){
				Id = Guid.NewGuid ().ToString();
			}*/

			// TODO: The DataConfig.Prefix static singleton should be moved to a normal object property
			Context.Data.Settings.Prefix = "TownSim-" + Context.Settings.EngineId;

            Summarizer = new GameConsoleSummarizer (Context);
		}

		public void Initialize()
		{
			if (Context.Settings.IsVerbose)
				Context.Console.WriteDebugLine ("Starting engine process");

            throw new NotImplementedException ();
            // TODO: Clean up this function. Most of this code is obsolete

			//EnsureTownsExist ();

			//if (Context.World.Towns.Length == 0)
			//	throw new Exception ("No towns have been added. Call the PopulateDefault() function.");

            //if (EnableDatabase)
			//    SaveInfo ();

			//RunCycles ();


			// TODO: Clean up
			//throw new NotImplementedException ();

			/*Console.WriteDebugLine ("Starting TownSim engine");

			Log.AppendLine (Id, "Starting engine");

			CreateTown ();

			SaveInfo ();

			RunCycles ();

			Dispose ();*/
		}

		public void EnsureTownsExist()
		{
			throw new NotImplementedException ();
			//if (Context.World.Towns.Length == 0)
			//	CreateTown ();
		}

		public void Initialize(Town town)
		{
			throw new NotImplementedException ();
			/*Console.WriteDebugLine ("Starting TownSim engine");

			AddTown (town);

			SaveInfo ();

			RunCycles ();

			Dispose ();*/
		}


		void Attach()
		{
			CurrentEngine.Add (this);

			CurrentEngine.Attach (Context.Info);

			throw new NotImplementedException ();
			//Settings.PlayerId = Player.Id;
		}

		public void AddTown(Town town)
		{
			if (town.People.Length == 0)
				throw new ArgumentException ("A town needs to have at least one person in it.");

			//Player = town.People [0];

			var list = new List<Town> ();
			if (Context.World.Towns != null)
				list.AddRange (Context.World.Towns);
			list.Add (town);
			Context.World.Towns = list.ToArray ();

			var peopleList = new List<Person> ();
			if (Context.World.People != null)
				peopleList.AddRange (Context.World.People);
			peopleList.AddRange (town.People);
			Context.World.People = peopleList.ToArray ();

			if (EnableDatabase) {
				Context.Data.Save (town);
			}
		}

		public void Run()
		{
			IsRunning = true;

			while (IsRunning)
			{
				var cycleStartTime = DateTime.Now;

				for (int x = 0; x < Context.Settings.GameSpeed; x++) {
					RunCycle (x+1);
				}

				if (Context.Settings.OutputType == ConsoleOutputType.Game) // TODO: Move this to settings
                    Summarizer.WriteSummary ();
			
				SleepUntilNextCycle(cycleStartTime);
			}
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

        // TODO: Remove if not needed
		//public void ShowSummary()
		//{
            /*var console = Context.Console;
			
			// Player.ValidateProperties ();
            console.ClearGame ();
            console.WriteGameLine ("TownSim Engine");
            console.WriteGameLine ("  Engine Id: " + Context.Settings.EngineId + "     Speed: " + Context.Settings.GameSpeed);
            console.WriteGameLine ("  Real clock: " + Clock.GetRealDurationString() + "   Game clock: " + Clock.GetGameDurationString());
			
            console.WriteGameLine ("  Player:");
            console.WriteGameLine ("    Age: " + Convert.ToInt32(Player.Age) + "    Gender:" + Player.Gender + "    Health:" + Player.Health);
            console.WriteGameLine ("    Thirst: " + Convert.ToInt32(Player.Thirst) + "   Hunger:" + Convert.ToInt32(Player.Hunger));
            console.WriteGameLine ("    Activity: " + Player.ActivityType);
            console.WriteGameLine ("    Home: " + (Player.Home != null ? Player.Home.PercentComplete : 0) + "%");

            console.WriteGameLine ();
            console.WriteGameLine ("   Priorities:");
            console.WriteGameLine ("     Water: " + (int)Player.Priorities[PriorityTypes.Water] + "%      Food: " + (int)Player.Priorities[PriorityTypes.Food] + "%     Shelter: " + (int)Player.Priorities[PriorityTypes.Shelter] + "%");

            console.WriteGameLine ();
            console.WriteGameLine ("   Supplies:");
            console.WriteGameLine ("     Water: " + (int)Player.Supplies[needTypes.Water] + "ml      Food: " + Player.Supplies[needTypes.Food] + " kgs     Wood: " + (int)Player.Supplies[NeedType.Wood] + "    Timber: " + (int)Player.Supplies[NeedType.Timber]);

            console.WriteGameLine ();
            console.WriteGameLine ("   Demands:");
            console.WriteGameLine ("     Water: " + (int)Player.GetDemandAmount(needTypes.Water) + "ml      Food: " + Player.GetDemandAmount(needTypes.Food) + " kgs     Wood: " + (int)Player.GetDemandAmount(NeedType.Wood) + "    Timber: " + (int)Player.GetDemandAmount(NeedType.Timber));

            console.WriteGameLine ();
*/
            /*Console.WriteGameLine ("  Towns:");

			foreach (var town in Towns) {
				town.ValidateProperties ();
				Console.WriteDebugLine ("    " + town.Name);
				Console.WriteDebugLine ("     People:");
				Console.WriteDebugLine ("       Population: " + town.Population + "   Males: " + town.TotalMales + "   Females: " + town.TotalFemales + "   Couples: " + town.TotalCouples + "  Births: " + town.TotalBirths + "   Deaths: " + town.TotalDeaths);
				Console.WriteDebugLine ("       Immigrants: " + town.TotalImmigrants + "  Emigrants: " + town.TotalEmigrants);
				Console.WriteDebugLine ("       Average age: " + String.Format("{0:0.##}", town.AverageAge));
				Console.WriteDebugLine ("       Homeless: " + town.TotalHomelessPeople);
				Console.WriteDebugLine ();
				Console.WriteDebugLine ("     Activities:");
				Console.WriteDebugLine ("       Active: " + town.TotalActive + "   Inactive: " + town.TotalInactive);
				Console.WriteDebugLine ();
				Console.WriteDebugLine ("     Forestry:");
				Console.WriteDebugLine ("       Trees: " + town.Trees.Length + "        Forestry workers: " + town.TotalForestryWorkers);
				Console.WriteDebugLine ("       Trees planted today: " + town.CountTreesPlantedToday(Clock.GameDuration) + "   Trees planted: " + town.TotalTreesPlanted + "    Trees being planted: " + town.TotalTreesBeingPlanted);
				Console.WriteDebugLine ("       Average tree size: " + (int)town.AverageTreeSize + "   Average tree age: " + (int)town.AverageTreeAge);
				Console.WriteDebugLine ();
				Console.WriteDebugLine ("     Garden:");
				Console.WriteDebugLine ("       Vegetables: " + town.Vegetables.Length);
				Console.WriteDebugLine ("       Gardeners: " + town.TotalGardeners);
				Console.WriteDebugLine ("       Average vegetable size: " + (int)town.AverageVegetableSize);
				Console.WriteDebugLine ("       Average vegetable age: " + (int)town.AverageVegetableAge);
				Console.WriteDebugLine ("       Vegetables planted today: " + town.CountVegetablesPlantedToday(Clock.GameDuration));
				Console.WriteDebugLine ("       Vegetables planted: " + town.TotalVegetablesPlanted);
				Console.WriteDebugLine ("       Vegetables being planted: " + town.TotalVegetablesBeingPlanted);
				Console.WriteDebugLine ("       Vegetables harvested today: " + town.CountVegetablesHarvestedToday(Clock.GameDuration));
				Console.WriteDebugLine ("       Vegetables harvested: " + town.TotalVegetablesHarvested);
				Console.WriteDebugLine ("       Vegetables being harvested: " + town.TotalVegetablesBeingHarvested);
				Console.WriteDebugLine ();
				Console.WriteDebugLine ("     Buildings:");
				Console.WriteDebugLine ("       Builders: " + town.TotalBuilders);
				Console.WriteDebugLine ("       Houses (complete): " + town.Buildings.TotalCompletedHouses);
				Console.WriteDebugLine ("       Houses (under const.): " + town.Buildings.TotalIncompleteHouses);
				Console.WriteDebugLine ("       Average percent complete: " + (int)town.Buildings.AveragePercentComplete);
				if (town.Alerts.Length > 0) {
					Console.WriteDebugLine ("     Alerts:");
					foreach (var alert in town.Alerts) {
						Console.WriteDebugLine ("       " + alert.Message);
					}
				}
				
			}*/
		//}

		public void Run(int numberOfCycles)
		{
			if (Context.Settings.IsVerbose)
				Context.Console.WriteDebugLine ("Running engine for " + numberOfCycles + " cycles.");

			for (int i = 0; i < numberOfCycles; i++)
			{
				RunCycle (i+1);
			}
		}

		public void RunCycle(int cycleNumber)
		{
			if (Context.Settings.IsVerbose) {
                Context.Console.WriteDebugLine ("");
                Context.Console.WriteDebugLine ("Starting game engine cycle... #" + cycleNumber);
                Context.Console.WriteDebugLine ("");
			}

			// TODO: Clean up
			//Effects.ApplySingleCycle ();

			// TODO: Remove if not needed
			//if (Context.World.Towns.Length == 0)
			//	throw new TownlessException ();

            ProcessEffects ();

			// People
            ProcessPeople();

			//foreach (var town in Context.World.Towns) {
			//	RunCycleForTown (town);
			//}

			//if (Context.World.Player.Health == 0)
			//	PlayerDied();

			// TODO: Remove
			/*if (totalPopulation == 0) {
				Console.WriteDebugLine ("Game over!");
				Console.ReadLine ();
				throw new PopulationExpiredException ();
			}*/

			if (Context.Settings.IsVerbose) {
                Context.Console.WriteDebugLine ("");
                Context.Console.WriteDebugLine ("Completed game engine cycle");
                Context.Console.WriteDebugLine ("");
			}
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

		public void RunCycleForTown(Town town)
		{
			var instructionEngine = new InstructionEngine ();

			instructionEngine.Update (town);

			//totalPopulation += town.Population;

			if (EnableDatabase)
				Context.Data.SaveOrUpdate (town);
		}

		public void RunCycleForPerson(Person person)
		{
			Persons.StartSingleCycle (person);


			// TODO: Remove. Obsolete
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
			throw new NotImplementedException ();

			// TODO: Move to player engine
			/*if (Settings.OutputType == ConsoleOutputType.Game)
				ShowSummary ();
			
			Console.WriteDebugLine ("The player died.");
			Console.WriteDebugLine ("Game Over");*/
		}

		public void Populate()
		{
            throw new NotImplementedException ();
            // TODO: Clean up
			//CreateTown();
		}

		public void Dispose()
		{
			Context.Data.Delete(Context.Info);
		}

		public event EventHandler Disposed;

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

