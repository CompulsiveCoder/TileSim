using System;
using townsim.Entities;
using townsim.Engine;

namespace townsim.EngineCustomConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var town = new Town ();

			Console.WriteLine ("Starting custom TownSim...");
			Console.WriteLine ("You'll be asked to provide some parameters for the simulation to run.");
			Console.WriteLine ("");
			Console.WriteLine ("New Sim");
			Console.Write("Population (default " + town.Population + "):");
			var populationEntry = Console.ReadLine ();
			Console.Write("Forests (default " + town.Forest + "):");
			var forestsEntry = Console.ReadLine ();
			Console.Write("Water (default " + town.WaterSources + "):");
			var waterEntry = Console.ReadLine ();
			Console.Write("Food (default " + town.FoodSources + "):");
			var foodEntry = Console.ReadLine ();

			try
			{
			if (!String.IsNullOrEmpty(populationEntry.Trim()))
				town.Population = Convert.ToInt32 (populationEntry);
			}
			catch {
				// Ignore and go with default
			}

			try
			{
				if (!String.IsNullOrEmpty(forestsEntry.Trim()))
					town.Forest = Convert.ToInt32 (forestsEntry);
			}
			catch {
				// Ignore and go with default
			}

			try
			{
				if (!String.IsNullOrEmpty(waterEntry.Trim()))
					town.WaterSources = Convert.ToInt32 (waterEntry);
			}
			catch {
				// Ignore and go with default
			}

			try
			{
				if (!String.IsNullOrEmpty(waterEntry.Trim()))
					town.FoodSources = Convert.ToInt32 (waterEntry);
			}
			catch {
				// Ignore and go with default
			}

			var engine = new townsimEngine ();
			engine.Start (town);
		}
	}
}
