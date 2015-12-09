using System;
using NUnit.Framework;
using townsim.Data;
using townsim.Data.Tests;
using townsim.Entities;
using townsim.Engine.Activities;

namespace townsim.Engine.Tests.Integration
{
	[TestFixture]
	public class ConstructionEngineTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Housing_1pop()
		{
			var settings = new EngineSettings (10);
			var constructionEngine = new BuildActivity (settings, new EngineClock(settings));

			var person = new Person ();

			var town = new Town (person);

			person.Town = town;

			person.ActivityType = ActivityType.Builder;
			constructionEngine.Update (person);

			Assert.AreEqual (1, town.TotalActive);

			for (int i = 0; i < 100; i++) {
				constructionEngine.Update (person);
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
			var settings = new EngineSettings (10);
			var constructionEngine = new BuildActivity (settings, new EngineClock(settings));
			var town = new Town (2);

			foreach (var person in town.People)
				person.ActivityType = ActivityType.Builder;

			constructionEngine.Update (town);
			Assert.AreEqual (2, town.TotalActive);

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
			var settings = new EngineSettings (10);
			var constructionEngine = new BuildActivity (settings, new EngineClock(settings));

			var town = new Town (5);

			foreach (var person in town.People)
				person.ActivityType = ActivityType.Builder;
			
			constructionEngine.Update (town);

			Assert.AreEqual (5, town.TotalActive);

			for (int i = 0; i < 1000; i++) {
				constructionEngine.Update (town);
			}

			var building = town.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, town.TotalBuilders);
			Assert.AreEqual (5, town.Buildings.TotalCompleted);
			Assert.AreEqual (5, town.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, town.Buildings.TotalIncompleteHouses);


		}
	}
}

