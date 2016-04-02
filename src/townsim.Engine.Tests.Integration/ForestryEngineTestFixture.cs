using System;
using townsim.Data.Tests;
using NUnit.Framework;
using townsim.Entities;
using townsim.Engine.Activities;

namespace townsim.Engine.Tests.Integration
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
			/*var town = new Town (1);
			town.TreesToPlantPerDay = 1;

			var settings = new EngineSettings (1);
			var forestryEngine = new PlantTreesActivity (settings, new EngineClock(settings));

			forestryEngine.HireWorkers (town);

			Assert.AreEqual (1, town.TotalForestryWorkers);*/
		}

		//[Test]
		public void Test_DoPlanting()
		{
			throw new NotImplementedException ();
			/*var town = new Town (1, 0);
			town.TreesToPlantPerDay = 1;

			var settings = new EngineSettings (1);
			var forestryEngine = new PlantTreesActivity (settings, new EngineClock(settings));

			// Workers need to be hired before planting can begin. Should this effect be mocked rather than using the HireWorkers function?
			forestryEngine.HireWorkers (town);

			forestryEngine.DoPlanting (town);

			var tree = town.Plants [0];

			var increment = forestryEngine.GetPlantingCompletionIncrement ();

			Assert.AreEqual (increment, tree.PercentPlanted);*/
		}
	}
}

