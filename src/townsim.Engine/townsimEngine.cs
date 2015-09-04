using System;
using townsim.Data;
using System.Threading;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class townsimEngine
	{
		public string Id { get;set; }

		/// <summary>
		/// The max time for each engine cycle in milliseconds.
		/// </summary>
		public int CycleTime = 1000;

		public DateTime GameStartTime = DateTime.MinValue;

		public Town[] Towns = new Town[]{};

		public bool EnableDatabase = true;

		public Person Player { get; set; }
		public Person[] People { get; set; }

		public townsimEngine ()
		{
			Initialize ();
		}

		public void Start()
		{
			Console.WriteLine ("Starting TownSim engine");

			CreateTown ();

			RunCycles ();
		}

		public void Start(Town town)
		{
			Console.WriteLine ("Starting TownSim engine");

			AddTown (town);

			RunCycles ();
		}

		void Initialize()
		{
			var guid = Guid.NewGuid ().ToString ();
			Id = guid.Substring (0, guid.IndexOf ("-"));
			DataConfig.Prefix = "TownSim-" + Id;
			GameStartTime = DateTime.Now;
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
				RunCycle ();
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
			Console.WriteLine ("  Clock: " + GetTimeString());
			Console.WriteLine ("  Player:");
			Console.WriteLine ("    Age:" + Convert.ToInt32(Player.Age));
			Console.WriteLine ("    Gender:" + Player.Gender);
			Console.WriteLine ("    Health:" + Player.Health);
			Console.WriteLine ("    Thirst:" + Convert.ToInt32(Player.Thirst));
			Console.WriteLine ("    Hunger:" + Convert.ToInt32(Player.Hunger));
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
				Console.WriteLine ("       Males: " + town.Statistics.TotalMales);
				Console.WriteLine ("       Females: " + town.Statistics.TotalFemales);
				Console.WriteLine ("       Births: " + town.TotalBirths);
				Console.WriteLine ("       Deaths: " + town.TotalDeaths);
				Console.WriteLine ("       Immigrants: " + town.TotalImmigrants);
				Console.WriteLine ("       Emigrants: " + town.TotalEmigrants);
				Console.WriteLine ("       Average age: " + String.Format("{0:0.##}", town.Statistics.AverageAge));
				Console.WriteLine ("       Homeless: " + town.Statistics.TotalHomelessPeople);
				Console.WriteLine ("       Breeding pairs: " + town.Statistics.TotalBreedingPairs);
				Console.WriteLine ();
				Console.WriteLine ("     Employment:");
				Console.WriteLine ("       Unemployed: " + town.Statistics.TotalUnemployed);
				Console.WriteLine ("       Employed: " + town.Statistics.TotalEmployed);
				Console.WriteLine ();
				Console.WriteLine ("     Resources:");
				Console.WriteLine ("       Water sources: " + Convert.ToInt32(town.WaterSources));
				Console.WriteLine ("       Food sources: " + Convert.ToInt32(town.FoodSources));
				Console.WriteLine ("       Forests: " + Convert.ToInt32(town.Forest));
				//Console.WriteLine ("     Timber: " + Convert.ToInt32(town.Timber));
				Console.WriteLine ();
				Console.WriteLine ("     Buildings:");
				Console.WriteLine ("       Builders: " + town.Statistics.TotalBuilders);
				Console.WriteLine ("       Houses (complete): " + town.Buildings.TotalCompletedHouses);
				Console.WriteLine ("       Houses (under const.): " + town.Buildings.TotalIncompleteHouses);
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
				var town = new Town ("Small Town", 3);
				AddTown (town);
			}
		}

		public void RunCycle()
		{
			var totalPopulation = 0;

			var saver = new TownSaver ();
			var thirstEngine = new ThirstEngine ();
			var hungerEngine = new HungerEngine ();
			var populationEngine = new PopulationEngine ();
			var forestsEngine = new ForestsEngine ();
			var waterSourcesEngine = new WaterSourcesEngine ();
			var constructionEngine = new ConstructionEngine ();
			//var foodEngine = new FoodEngine ();

			if (Towns.Length == 0)
				throw new TownlessException ();

			// Local people
			foreach (var person in People) {

				hungerEngine.Update (person);
				thirstEngine.Update (person);
			}

			foreach (var town in Towns) {
				totalPopulation += town.Population;


				// Local environment
				waterSourcesEngine.Update (town);
				forestsEngine.Update (town);


				// Local civil
				constructionEngine.Update (town);

				// Global, population and migration
				populationEngine.Update(town);

				if (Player.Health == 0)
					throw new PlayerDiedException ();

				if (EnableDatabase)
					saver.Save (town);
			}

			if (totalPopulation == 0) {
				Console.WriteLine ("Game over!");
				Console.ReadLine ();
				throw new PopulationExpiredException ();
			}
		}

		public string GetTimeString(TimeSpan timeSpan)
		{
			string answer;
			if (timeSpan.TotalMinutes < 1.0)
			{
				answer = String.Format("{0}s", timeSpan.Seconds);
			}
			else if (timeSpan.TotalHours < 1.0)
			{
				answer = String.Format("{0}m:{1:D2}s", timeSpan.Minutes, timeSpan.Seconds);
			}
			else // more than 1 hour
			{
				answer = String.Format("{0}h:{1:D2}m:{2:D2}s", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
			}

			return answer;
		}

		public string GetTimeString(int secs)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(secs);
			return GetTimeString (timeSpan);
		}

		public string GetTimeString()
		{
			return GetTimeString(DateTime.Now.Subtract (GameStartTime));
		}

	}
}

