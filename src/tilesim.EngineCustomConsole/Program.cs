using System;
using tilesim.Engine.Entities;
using tilesim.Engine;

namespace tilesim.EngineCustomConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            throw new NotImplementedException ();
			//var tile = new Tile ();

			/*Console.WriteDebugLine ("Starting custom TileSim...");
			Console.WriteDebugLine ("You'll be asked to provide some parameters for the simulation to run.");
			Console.WriteDebugLine ("");
			Console.WriteDebugLine ("New Sim");
			Console.Write("Population (default " + tile.Population + "):");
			var populationEntry = Console.ReadLine ();
			Console.Write("Forests (default " + tile.Forest + "):");
			var forestsEntry = Console.ReadLine ();
			Console.Write("Water (default " + tile.WaterSources + "):");
			var waterEntry = Console.ReadLine ();
			Console.Write("Food (default " + tile.FoodSources + "):");
			var foodEntry = Console.ReadLine ();

			try
			{
				var personCreator = new PersonCreator();
				if (!String.IsNullOrEmpty(populationEntry.Trim()))
				{
					for (int i = 0; i < Convert.ToInt32(populationEntry); i++)
					{
						tile.People = personCreator.CreateAdults(Convert.ToInt32(populationEntry));
					}
				}
			}
			catch {
				// Ignore and go with default
			}

			try
			{
				if (!String.IsNullOrEmpty(forestsEntry.Trim()))
					tile.CreateTrees(Convert.ToInt32(forestsEntry));
			}
			catch {
				// Ignore and go with default
			}

			try
			{
				if (!String.IsNullOrEmpty(waterEntry.Trim()))
					tile.WaterSources = Convert.ToInt32 (waterEntry);
			}
			catch {
				// Ignore and go with default
			}

			try
			{
				if (!String.IsNullOrEmpty(waterEntry.Trim()))
					tile.FoodSources = Convert.ToInt32 (foodEntry);
			}
			catch {
				// Ignore and go with default
			}
*/
			/*var engine = new EngineProcess ();
			engine.Start (tile);*/
		}
	}
}
