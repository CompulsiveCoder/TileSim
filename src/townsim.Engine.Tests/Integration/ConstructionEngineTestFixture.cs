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
			var town = new Town (1);
			constructionEngine.Update (town);
			Assert.AreEqual (1, town.TotalEmployed);

			for (int i = 0; i < 100; i++) {
				constructionEngine.Update (town);
			}

			var building = town.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, town.TotalBuilders);
			Assert.AreEqual (1, town.Buildings.TotalCompleted);
			Assert.AreEqual (1, town.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, town.Buildings.TotalIncompleteHouses);

		}

		[Test]
		public void Test_Housing_2pop()
		{
			var constructionEngine = new ConstructionEngine ();
			var town = new Town (2);
			constructionEngine.Update (town);
			Assert.AreEqual (2, town.TotalEmployed);

			for (int i = 0; i < 100; i++) {
				constructionEngine.Update (town);
			}

			var building = town.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, town.TotalBuilders);
			Assert.AreEqual (2, town.Buildings.TotalCompleted);
			Assert.AreEqual (2, town.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, town.Buildings.TotalIncompleteHouses);

		}

		[Test]
		public void Test_Housing_5pop()
		{
			var constructionEngine = new ConstructionEngine ();
			var town = new Town (5);
			constructionEngine.Update (town);
			Assert.AreEqual (5, town.TotalEmployed);

			for (int i = 0; i < 1000; i++) {
				constructionEngine.Update (town);
			}

			var building = town.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, town.TotalBuilders);
			Assert.AreEqual (5, town.Buildings.TotalCompleted);
			Assert.AreEqual (5, town.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, town.Buildings.TotalIncompleteHouses);


			//population
		}
	}
}

