using System;
using townsim.Data;
using System.Threading;
using townsim.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace townsim.Engine
{
	public class townsimEngine : IComponent
	{
		public Guid Id { get;set; }

		/// <summary>
		/// The max time for each engine cycle in milliseconds.
		/// </summary>
		public int CycleTime = 2000;

		public DateTime GameStartTime = DateTime.MinValue;

		public Town[] Towns = new Town[]{};

		public bool EnableDatabase = true;

		public Person Player { get; set; }
		public Person[] People { get; set; }

		public LogWriter Log = new LogWriter ();

		public EngineSettings Settings;

		public EngineClock Clock;

		public townsimEngine ()
		{
			Initialize ();
		}

		public townsimEngine (Guid engineId)
		{
			Id = engineId;

			Initialize ();
		}

		public void Start()
		{
			Console.WriteLine ("Starting TownSim engine");

			Log.AppendLine (Id, "Starting engine");

			CreateTown ();

			Attach ();
			SaveInfo ();

			RunCycles ();

			Dispose ();
		}

		public void Start(Town town)
		{
			Console.WriteLine ("Starting TownSim engine");

			AddTown (town);

			Attach ();
			SaveInfo ();

			RunCycles ();

			Dispose ();
		}

		void Initialize()
		{
			Settings = new EngineSettings ();
			Clock = new EngineClock (Settings);

			if (Id == Guid.Empty){
				Id = Guid.NewGuid ();
			}

			DataConfig.Prefix = "TownSim-" + Id;
			GameStartTime = DateTime.Now;

			var idManager = new EngineIdManager ();
			idManager.Add (Id);

		}

		void Attach()
		{
			CurrentEngine.Add (this);

      		var engineInfo = new EngineInfo (Id, Clock.StartTime, Settings, Player.Id);
 
			CurrentEngine.Attach (engineInfo);
		}

		public void SaveInfo()
		{
      		var engineInfo = new EngineInfo (Id, Clock.StartTime, Settings, Player.Id);

			var saver = new EngineInfoSaver ();
			saver.Save (engineInfo);
		}

		public void AddTown(Town town)
		{
			if (town.People.Length == 0)
				throw new ArgumentException ("A town needs to have at least one person in it.");

			Player = town.People [0];

			var list = new List<Town> ();
			if (Towns != null)
				list.AddRange (Towns);
			list.Add (town);
			Towns = list.ToArray ();

			var peopleList = new List<Person> ();
			peopleList.AddRange (town.People);
			People = peopleList.ToArray ();

			if (EnableDatabase) {
				var townSaver = new TownSaver ();
				townSaver.Save (town);
			}
		}

		void RunCycles()
		{
			for (int i = 0; i < 1000; i++) {
				var beforeTime = DateTime.Now;
				for (int x = 0; x < Settings.GameSpeed; x++) {
					RunCycle ();
				}
				ShowSummary ();
				var afterTime = DateTime.Now;
				var duration = afterTime.Subtract (beforeTime);
				Console.WriteLine ("Duration: " + duration.Milliseconds + " milliseconds (max " + CycleTime + ")");
				var sleepDuration = CycleTime - duration.Milliseconds;
				if (sleepDuration > 0)
					Thread.Sleep (sleepDuration);
			}
		}

		public void ShowSummary()
		{

			Player.ValidateProperties ();
			Console.Clear ();
			Console.WriteLine ("TownSim Engine");
			Console.WriteLine ("  Id: " + Id);
			Console.WriteLine ("  Real clock: " + Clock.GetRealDurationString());
			Console.WriteLine ("  Game clock: " + Clock.GetGameDurationString());
			Console.WriteLine ("  Player:");
			Console.WriteLine ("    Age:" + Convert.ToInt32(Player.Age));
			Console.WriteLine ("    Gender:" + Player.Gender);
			Console.WriteLine ("    Health:" + Player.Health);
			Console.WriteLine ("    Thirst:" + Convert.ToInt32(Player.Thirst));
			Console.WriteLine ("    Hunger:" + Convert.ToInt32(Player.Hunger));
			Console.WriteLine ("    Activity:" + Player.Activity);
			Console.WriteLine ("    Home:" + (Player.Home != null ? Player.Home.PercentComplete : 0) + "%");
			Console.WriteLine ("  Towns:");

			foreach (var town in Towns) {
				town.ValidateProperties ();
				Console.WriteLine ("    " + town.Name);
				Console.WriteLine ("     People:");
				Console.WriteLine ("       Pop.: " + town.Population);
				//foreach (var person in town.People) {
					//Console.WriteLine ("         Age: " + String.Format("{0:0.##}", person.Age));
				//	Console.WriteLine ("         Age: " + Convert.ToInt32(person.Age));
				//	Console.WriteLine ("         Gender: " + person.Gender);
				//}
				Console.WriteLine ("       Males: " + town.TotalMales);
				Console.WriteLine ("       Females: " + town.TotalFemales);
				Console.WriteLine ("       Births: " + town.TotalBirths);
				Console.WriteLine ("       Deaths: " + town.TotalDeaths);
				Console.WriteLine ("       Immigrants: " + town.TotalImmigrants);
				Console.WriteLine ("       Emigrants: " + town.TotalEmigrants);
				Console.WriteLine ("       Average age: " + String.Format("{0:0.##}", town.AverageAge));
				Console.WriteLine ("       Homeless: " + town.TotalHomelessPeople);
				Console.WriteLine ("       Couples: " + town.TotalBreedingPairs);
				Console.WriteLine ();
				Console.WriteLine ("     Employment:");
				Console.WriteLine ("       Unemployed: " + town.TotalUnemployed);
				Console.WriteLine ("       Employed: " + town.TotalEmployed);
				Console.WriteLine ();
				Console.WriteLine ("     Resources:");
				Console.WriteLine ("       Water sources: " + (int)town.WaterSources + " litres");
				Console.WriteLine ("       Food sources: " + (int)town.FoodSources + " kgs");
				Console.WriteLine ("       Timber: " + (int)town.Timber);
				Console.WriteLine ();
				Console.WriteLine ("     Forestry:");
				Console.WriteLine ("       Trees: " + town.Trees.Length);
				Console.WriteLine ("       Forestry workers: " + town.TotalForestryWorkers);
				Console.WriteLine ("       Trees planted today: " + town.CountTreesPlantedToday(Clock.GameDuration));
				Console.WriteLine ("       Trees planted: " + town.TotalTreesPlanted);
				Console.WriteLine ("       Trees being planted: " + town.TotalTreesBeingPlanted);
				Console.WriteLine ("       Average tree size: " + (int)town.AverageTreeSize);
				Console.WriteLine ("       Average tree age: " + (int)town.AverageTreeAge);
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
				/*foreach (var building in town.Buildings) {
					if (!building.IsCompleted) {
						Console.WriteLine ("       " + building.Type);
						Console.WriteLine ("           Is Compl.: " + building.IsCompleted);
						Console.WriteLine ("           % Compl.: " + building.PercentComplete);
						Console.WriteLine ("           Workers: " + building.WorkerCount);
						Console.WriteLine ("           Timber: " + building.TimberAvailable + "/" + building.TimberCost);
					}
				}*/
				if (town.Alerts.Length > 0) {
					Console.WriteLine ("     Alerts:");
					foreach (var alert in town.Alerts) {
						Console.WriteLine ("       " + alert.Message);
					}
				}
				
			}
		}

		public void CreateTown()
		{
			var indexer = new TownIndexer ();
			var towns = indexer.Get ();

			if (towns.Length < 1)
			{
				var town = new Town ("Small Town", Town.DefaultPopulation);
				AddTown (town);
			}
		}

		public void RunCycle()
		{
			var totalPopulation = 0;

			var saver = new TownSaver ();

			var instructionEngine = new InstructionEngine ();
			var thirstEngine = new ThirstEngine (Settings);
			var hungerEngine = new HungerEngine (Settings);
			var populationEngine = new PopulationEngine ();
			var forestsEngine = new ForestsEngine (Settings);
			var forestryEngine = new ForestryEngine (Settings, Clock);
			var waterSourcesEngine = new WaterSourcesEngine (Settings);
			var constructionEngine = new ConstructionEngine (Settings, Clock);
			var plantEngine = new PlantEngine ();
			var gardenEngine = new GardeningEngine (Settings, Clock);
			var harvestingEngine = new HarvestingEngine (Settings, Clock);
			var healthEngine = new HealthEngine ();
			var choiceEngine = new ChoiceEngine ();
			//var foodEngine = new FoodEngine ();

			if (Towns.Length == 0)
				throw new TownlessException ();

			// Local people
			foreach (var person in People) {

				hungerEngine.Update (person);
				thirstEngine.Update (person);
				healthEngine.Update (person);
				choiceEngine.Update (person);
			}

			var plants = new List<Plant> ();

			foreach (var town in Towns) {
				instructionEngine.Update (town);

				plants.AddRange (town.Plants);
				totalPopulation += town.Population;


				// Local environment
				waterSourcesEngine.Update (town);
				forestsEngine.Update (town);


				// Local civil
				constructionEngine.Update (town);

				gardenEngine.Update (town);
				forestryEngine.Update (town);
				harvestingEngine.Update (town);

				// Global, population and migration
				populationEngine.Update(town);

				if (Player.Health == 0)
					PlayerDied();

				if (EnableDatabase)
					saver.Save (town);
			}

			// Local people
			foreach (var plant in plants) {

				plantEngine.Update (plant);
			}

			if (totalPopulation == 0) {
				Console.WriteLine ("Game over!");
				Console.ReadLine ();
				throw new PopulationExpiredException ();
			}
		}

		public void PlayerDied()
		{
			ShowSummary ();
			Console.WriteLine ("The player died.");
			Console.WriteLine ("Game Over");
		}

		public void Dispose()
		{
			var idManager = new EngineIdManager ();
			idManager.Remove (Id);
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

