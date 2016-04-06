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
		/*
		public EngineClock Clock;

		public EngineSettings Settings { get; set; }

		public DataManager Data { get;set; }

		public EngineInfo Info { get; set; }*/

		public bool IsRunning;

		public EffectEngine Effects { get; set; }

		public PersonEngine Persons { get; set; }

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
				Console.WriteLine ("Constructing game engine process");

			//Data = data;			
			//Settings = settings;
			//Clock = new EngineClock (Context.Settings);
			//Info = new EngineInfo (Clock.StartTime, Settings);
			Effects = new EffectEngine (context);
			Persons = new PersonEngine (context);

			// TODO: Check if needed
			/*if (String.IsNullOrEmpty(Id)){
				Id = Guid.NewGuid ().ToString();
			}*/

			// TODO: The DataConfig.Prefix static singleton should be moved to a normal object property
			Context.Data.Settings.Prefix = "TownSim-" + Context.Settings.EngineId;
		}

		public void Initialize()
		{
			if (Context.Settings.IsVerbose)
				Console.WriteLine ("Starting engine process");

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

			/*Console.WriteLine ("Starting TownSim engine");

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
			/*Console.WriteLine ("Starting TownSim engine");

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
					ShowSummary ();
			
				SleepUntilNextCycle(cycleStartTime);
			}
		}

		public void SleepUntilNextCycle(DateTime cycleStartTime)
		{
			var cycleCompleteTime = DateTime.Now;

			var cycleDuration = cycleCompleteTime.Subtract (cycleStartTime);

			Console.WriteLine ("Duration: " + cycleDuration.Milliseconds + " milliseconds (max " + Context.Settings.CycleDuration + ")");

			var sleepDurationInMilliseconds = Context.Settings.CycleDuration - cycleDuration.Milliseconds;
			if (sleepDurationInMilliseconds > 0)
				Thread.Sleep (sleepDurationInMilliseconds);
		}

		public void ShowSummary()
		{

			/*
			 Player.ValidateProperties ();
			Console.Clear ();
			Console.WriteLine ("TownSim Engine");
			Console.WriteLine ("  Id: " + Id + "     Speed: " + Settings.GameSpeed);
			Console.WriteLine ("  Real clock: " + Clock.GetRealDurationString() + "   Game clock: " + Clock.GetGameDurationString());
			
			Console.WriteLine ("  Player:");
			Console.WriteLine ("    Age: " + Convert.ToInt32(Player.Age) + "    Gender:" + Player.Gender + "    Health:" + Player.Health);
			Console.WriteLine ("    Thirst: " + Convert.ToInt32(Player.Thirst) + "   Hunger:" + Convert.ToInt32(Player.Hunger));
			Console.WriteLine ("    Activity: " + Player.ActivityType);
			Console.WriteLine ("    Home: " + (Player.Home != null ? Player.Home.PercentComplete : 0) + "%");

			Console.WriteLine ();
			Console.WriteLine ("   Priorities:");
			Console.WriteLine ("     Water: " + (int)Player.Priorities[PriorityTypes.Water] + "%      Food: " + (int)Player.Priorities[PriorityTypes.Food] + "%     Shelter: " + (int)Player.Priorities[PriorityTypes.Shelter] + "%");

			Console.WriteLine ();
			Console.WriteLine ("   Supplies:");
			Console.WriteLine ("     Water: " + (int)Player.Supplies[needTypes.Water] + "ml      Food: " + Player.Supplies[needTypes.Food] + " kgs     Wood: " + (int)Player.Supplies[NeedType.Wood] + "    Timber: " + (int)Player.Supplies[NeedType.Timber]);

			Console.WriteLine ();
			Console.WriteLine ("   Demands:");
			Console.WriteLine ("     Water: " + (int)Player.GetDemandAmount(needTypes.Water) + "ml      Food: " + Player.GetDemandAmount(needTypes.Food) + " kgs     Wood: " + (int)Player.GetDemandAmount(NeedType.Wood) + "    Timber: " + (int)Player.GetDemandAmount(NeedType.Timber));

			Console.WriteLine ();

			Console.WriteLine ("  Towns:");

			foreach (var town in Towns) {
				town.ValidateProperties ();
				Console.WriteLine ("    " + town.Name);
				Console.WriteLine ("     People:");
				Console.WriteLine ("       Population: " + town.Population + "   Males: " + town.TotalMales + "   Females: " + town.TotalFemales + "   Couples: " + town.TotalCouples + "  Births: " + town.TotalBirths + "   Deaths: " + town.TotalDeaths);
				Console.WriteLine ("       Immigrants: " + town.TotalImmigrants + "  Emigrants: " + town.TotalEmigrants);
				Console.WriteLine ("       Average age: " + String.Format("{0:0.##}", town.AverageAge));
				Console.WriteLine ("       Homeless: " + town.TotalHomelessPeople);
				Console.WriteLine ();
				Console.WriteLine ("     Activities:");
				Console.WriteLine ("       Active: " + town.TotalActive + "   Inactive: " + town.TotalInactive);
				Console.WriteLine ();
				Console.WriteLine ("     Forestry:");
				Console.WriteLine ("       Trees: " + town.Trees.Length + "        Forestry workers: " + town.TotalForestryWorkers);
				Console.WriteLine ("       Trees planted today: " + town.CountTreesPlantedToday(Clock.GameDuration) + "   Trees planted: " + town.TotalTreesPlanted + "    Trees being planted: " + town.TotalTreesBeingPlanted);
				Console.WriteLine ("       Average tree size: " + (int)town.AverageTreeSize + "   Average tree age: " + (int)town.AverageTreeAge);
				Console.WriteLine ();
				Console.WriteLine ("     Garden:");
				Console.WriteLine ("       Vegetables: " + town.Vegetables.Length);
				Console.WriteLine ("       Gardeners: " + town.TotalGardeners);
				Console.WriteLine ("       Average vegetable size: " + (int)town.AverageVegetableSize);
				Console.WriteLine ("       Average vegetable age: " + (int)town.AverageVegetableAge);
				Console.WriteLine ("       Vegetables planted today: " + town.CountVegetablesPlantedToday(Clock.GameDuration));
				Console.WriteLine ("       Vegetables planted: " + town.TotalVegetablesPlanted);
				Console.WriteLine ("       Vegetables being planted: " + town.TotalVegetablesBeingPlanted);
				Console.WriteLine ("       Vegetables harvested today: " + town.CountVegetablesHarvestedToday(Clock.GameDuration));
				Console.WriteLine ("       Vegetables harvested: " + town.TotalVegetablesHarvested);
				Console.WriteLine ("       Vegetables being harvested: " + town.TotalVegetablesBeingHarvested);
				Console.WriteLine ();
				Console.WriteLine ("     Buildings:");
				Console.WriteLine ("       Builders: " + town.TotalBuilders);
				Console.WriteLine ("       Houses (complete): " + town.Buildings.TotalCompletedHouses);
				Console.WriteLine ("       Houses (under const.): " + town.Buildings.TotalIncompleteHouses);
				Console.WriteLine ("       Average percent complete: " + (int)town.Buildings.AveragePercentComplete);
				if (town.Alerts.Length > 0) {
					Console.WriteLine ("     Alerts:");
					foreach (var alert in town.Alerts) {
						Console.WriteLine ("       " + alert.Message);
					}
				}
				
			}*/
		}

		public void Run(int numberOfCycles)
		{
			if (Context.Settings.IsVerbose)
				Console.WriteLine ("Running engine for " + numberOfCycles + " cycles.");

			for (int i = 0; i < numberOfCycles; i++)
			{
				RunCycle (i+1);
			}
		}

		public void RunCycle(int cycleNumber)
		{
			if (Context.Settings.IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Starting game engine cycle... #" + cycleNumber);
				Console.WriteLine ("");
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
				Console.WriteLine ("Game over!");
				Console.ReadLine ();
				throw new PopulationExpiredException ();
			}*/

			if (Context.Settings.IsVerbose) {
				Console.WriteLine ("");
				Console.WriteLine ("Completed game engine cycle");
				Console.WriteLine ("");
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
			
			Console.WriteLine ("The player died.");
			Console.WriteLine ("Game Over");*/
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

