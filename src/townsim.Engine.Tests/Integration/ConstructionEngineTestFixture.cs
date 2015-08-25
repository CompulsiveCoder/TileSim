using System;
using NUnit.Framework;
using townsim.Data;
using townsim.Data.Tests;
using townsim.Entities;

namespace townsim.Engine.Tests
{
	[TestFixture]
	public class ConstructionEngineTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Housing_1pop()
		{
			var constructionEngine = new ConstructionEngine ();
			var town = new Town ();
			town.Population = 1;
			constructionEngine.Update (town);
			Assert.AreEqual (1, town.Workers);

			for (int i = 0; i < 100; i++) {
				constructionEngine.Update (town);
			}

			Assert.AreEqual (0, town.Workers);
			Assert.AreEqual (1, town.Buildings.TotalCompleted);
			Assert.AreEqual (1, town.Buildings.TotalCompletedHouses);


			//population
		}

		[Test]
		public void Test_Housing_5pop()
		{
			var constructionEngine = new ConstructionEngine ();
			var town = new Town ();
			town.Population = 5;
			constructionEngine.Update (town);
			Assert.AreEqual (5, town.Workers);

			for (int i = 0; i < 100; i++) {
				constructionEngine.Update (town);
			}

			Assert.AreEqual (0, town.Workers);
			Assert.AreEqual (5, town.Buildings.TotalCompleted);
			Assert.AreEqual (5, town.Buildings.TotalCompletedHouses);


			//population
		}
	}
}

