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

			CreateTowns ();

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
			saver.Save (new Town ("TestTown", 100));
			saver.Save (new Town ("AnotherTown", 100));
		}

		public void RunCycle()
		{
			var indexer = new TownIndexer ();
			var towns = indexer.Get ();
			var saver = new TownSaver ();
			foreach (var town in towns) {
				UpdatePopulation (town);
				saver.Save (town);
			}
		}

		public void UpdatePopulation(Town town)
		{
			UpdatePopulationBirthRate (town);
			UpdatePopulationMigration (town);
		}

		public void UpdatePopulationBirthRate(Town town)
		{
			if (town.Population > 100)
				town.Population += town.Population / 50;
			else
				town.Population += town.Population / 20;
		}

		public void UpdatePopulationMigration(Town town)
		{
			var probability = new Random ().Next (100);
			if (probability > 90)
			{
				var value = new Random ().Next (5);
				town.Population += value;
			}
		}
	}
}

