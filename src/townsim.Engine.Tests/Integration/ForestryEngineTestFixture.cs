using System;
using townsim.Data.Tests;
using NUnit.Framework;
using townsim.Entities;

namespace townsim.Engine.Tests
{
	[TestFixture]
	public class ForestryEngineTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_HireForestryWorkers()
		{
			var town = new Town (1);
			town.TreesToPlantPerDay = 1;

			var settings = new EngineSettings (1);
			var forestryEngine = new ForestryEngine (settings, new EngineClock(settings));

			forestryEngine.HireWorkers (town);

			Assert.AreEqual (1, town.TotalForestryWorkers);
		}

		[Test]
		public void Test_DoPlanting()
		{
			var town = new Town (1, 0);
			town.TreesToPlantPerDay = 1;

			var settings = new EngineSettings (1);
			var forestryEngine = new ForestryEngine (settings, new EngineClock(settings));

			// Workers need to be hired before planting can begin. Should this effect be mocked rather than using the HireWorkers function?
			forestryEngine.HireWorkers (town);

			forestryEngine.DoPlanting (town);

			var tree = town.Plants [0];

			var increment = forestryEngine.GetPlantingCompletionIncrement ();

			Assert.AreEqual (increment, tree.PercentPlanted);
		}
	}
}

