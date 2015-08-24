using System;
using townsim.Data;
using System.Threading;

namespace townsim.EngineConsole
{
	public class Engine
	{
		public Engine ()
		{
		}

		public void Start()
		{
			Console.WriteLine ("Starting TownSim engine");

			//CreateTowns ();

			for (int i = 0; i < 100; i++) {
				RunCycle ();
				ShowTownSummary ();
				Thread.Sleep (1000);
			}
		}

		public void ShowTownSummary()
		{
			Console.Clear ();
			Console.WriteLine ("TownSim Engine");
			Console.WriteLine (DateTime.Now.ToString());
			Console.WriteLine ("  Towns:");
			var indexer = new TownIndexer ();
			var towns = indexer.Get ();
			foreach (var town in towns) {
				Console.WriteLine ("    " + town.Name + " - p" + town.Population);
			}
		}

		public void CreateTowns()
		{

			var saver = new TownSaver ();
			var smallTown = new Town ("Small Town", 120);
			smallTown.WaterSources = 20000;
			smallTown.Forest = 40000;
			saver.Save (smallTown);
			var largeTown = new Town ("Large Town", 23000);
			smallTown.WaterSources = 30000;
			smallTown.Forest = 5000;
			saver.Save (largeTown);
		}

		public void RunCycle()
		{
			var indexer = new TownIndexer ();
			var towns = indexer.Get ();
			var saver = new TownSaver ();
			var populationEngine = new PopulationEngine ();
			var forestsEngine = new ForestsEngine ();
			var waterSourcesEngine = new WaterSourcesEngine ();
			foreach (var town in towns) {
				populationEngine.Update(town);
				waterSourcesEngine.Update (town);
				forestsEngine.Update (town);
				saver.Save (town);
			}
		}


	}
}

