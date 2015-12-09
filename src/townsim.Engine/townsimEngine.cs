using System;
using townsim.Data;
using System.Threading;
using townsim.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using datamanager.Data;
using townsim.Engine.Activities;
using townsim.Engine.Effects;

namespace townsim.Engine
{
	public class townsimEngine : IComponent
	{
		public string Id { get;set; }

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

		public EngineInfo Info
		{
			get
			{
				return new EngineInfo (Id, Clock.StartTime, Settings, Player.Id);
			}
		}

		public townsimEngine ()
		{
			Initialize ();
		}

		public townsimEngine (string engineId)
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

			if (String.IsNullOrEmpty(Id)){
				Id = Guid.NewGuid ().ToString();
			}

			DataConfig.Prefix = "TownSim-" + Id;
			GameStartTime = DateTime.Now;
		}

		void Attach()
		{
			CurrentEngine.Add (this);

			CurrentEngine.Attach (Info);
		}

		public void SaveInfo()
		{
			var data = new DataManager ();
			data.Save (Info);
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
				var data = new DataManager ();
				data.Save (town);
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
			Console.WriteLine ("     Water: " + (int)Player.Supplies[SupplyTypes.Water] + "ml      Food: " + Player.Supplies[SupplyTypes.Food] + " kgs     Timber: " + (int)Player.Supplies[SupplyTypes.Timber]);

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
			var towns = new DataManager().Get<Town> ();

			if (towns.Length < 1)
			{
				var town = new Town ("Small Town", Town.DefaultPopulation);
				AddTown (town);
			}
		}

		public void RunCycle()
		{
			var totalPopulation = 0;

			var data = new DataManager ();

			var instructionEngine = new InstructionEngine ();

			var thirstEffect = new ThirstEffect (Settings);
			var hungerEffect = new HungerEffect (Settings);
			var populationEffect = new PopulationEffect ();
			var treeGrowthEffect = new TreeGrowthEffect (Settings);
			var rainEffect = new RainEffect (Settings);
			var plantGrowthEffect = new PlantGrowthEffect ();

			var shelterPrioritizer = new ShelterPrioritizer ();
			var foodPrioritizer = new FoodPrioritizer ();
			var waterPrioritizer = new WaterPrioritizer ();

			var healthEffect = new HealthEffect ();

			var plantTreesActivity = new PlantTreesActivity (Settings, Clock);
			var gardenActivity = new GardenActivity (Settings, Clock);
			var harvestingActivity = new HarvestActivity (Settings, Clock);

			if (Towns.Length == 0)
				throw new TownlessException ();

			// People
			foreach (var person in People) {
				hungerEffect.Update (person);
				thirstEffect.Update (person);
				healthEffect.Update (person);

				shelterPrioritizer.Prioritize (person);
				foodPrioritizer.Prioritize (person);
				waterPrioritizer.Prioritize (person);

				Act (person);
			}

			foreach (var town in Towns) {
				instructionEngine.Update (town);

				totalPopulation += town.Population;


				// Local environment
				rainEffect.Update (town);
				treeGrowthEffect.Update (town);


				// Global, population and migration
				populationEffect.Update(town);


				// Local people
				foreach (var plant in town.Plants) {
					plantGrowthEffect.Update (plant);
				}

				if (Player.Health == 0)
					PlayerDied();

				if (EnableDatabase)
					data.Save (town);
			}


			if (totalPopulation == 0) {
				Console.WriteLine ("Game over!");
				Console.ReadLine ();
				throw new PopulationExpiredException ();
			}
		}

		public void Act(Person person)
		{
			new PersonEngine (Settings, Clock).Act (person);
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
			ShowSummary ();
			Console.WriteLine ("The player died.");
			Console.WriteLine ("Game Over");
		}

		public void Dispose()
		{
			new DataManager().Delete(Info);
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

