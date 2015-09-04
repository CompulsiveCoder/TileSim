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

			Assert.AreEqual (0, town.TotalEmployed);
			Assert.AreEqual (1, town.Buildings.TotalCompleted);
			Assert.AreEqual (1, town.Buildings.TotalCompletedHouses);


			//population
		}

		[Test]
		public void Test_Housing_5pop()
		{
			throw new NotImplementedException ();
			var constructionEngine = new ConstructionEngine ();
			var town = new Town (5);
			constructionEngine.Update (town);
			Assert.AreEqual (5, town.TotalEmployed);

			for (int i = 0; i < 100; i++) {
				constructionEngine.Update (town);
			}

			Assert.AreEqual (0, town.TotalEmployed);
			Assert.AreEqual (5, town.Buildings.TotalCompleted);
			Assert.AreEqual (5, town.Buildings.TotalCompletedHouses);


			//population
		}
	}
}

