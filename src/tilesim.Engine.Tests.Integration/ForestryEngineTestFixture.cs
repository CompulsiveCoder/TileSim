using System;
using tilesim.Data.Tests;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Tests.Integration
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class ForestryEngineTestFixture : BaseDataTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_HireForestryWorkers()
		{
			throw new NotImplementedException ();
			/*var tile = new Tile (1);
			tile.TreesToPlantPerDay = 1;

			var settings = new EngineSettings (1);
			var forestryEngine = new PlantTreesActivity (settings, new EngineClock(settings));

			forestryEngine.HireWorkers (tile);

			Assert.AreEqual (1, tile.TotalForestryWorkers);*/
		}

		//[Test]
		public void Test_DoPlanting()
		{
			throw new NotImplementedException ();
			/*var tile = new Tile (1, 0);
			tile.TreesToPlantPerDay = 1;

			var settings = new EngineSettings (1);
			var forestryEngine = new PlantTreesActivity (settings, new EngineClock(settings));

			// Workers need to be hired before planting can begin. Should this effect be mocked rather than using the HireWorkers function?
			forestryEngine.HireWorkers (tile);

			forestryEngine.DoPlanting (tile);

			var tree = tile.Plants [0];

			var increment = forestryEngine.GetPlantingCompletionIncrement ();

			Assert.AreEqual (increment, tree.PercentPlanted);*/
		}
	}
}

