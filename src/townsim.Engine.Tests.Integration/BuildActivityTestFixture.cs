using System;
using NUnit.Framework;
using townsim.Data;
using townsim.Data.Tests;
using townsim.Engine.Entities;
using townsim.Engine.Activities;

namespace townsim.Engine.Tests.Integration.Activities
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class BuildActivityTestFixture : BaseDataTestFixture
	{
		//[Test]
		public void Test_Housing_1pop()
		{
			throw new NotImplementedException ();
			/*var settings = new EngineSettings (10);
			var buildActivity = new BuildActivity (settings, new EngineClock(settings));

			var person = new Person ();

			var town = new Town (person);

			person.Town = town;

			person.ActivityType = ActivityType.Builder;
			buildActivity.Act ();

			Assert.AreEqual (1, town.TotalActive);

			for (int i = 0; i < 100; i++) {
				buildActivity.Act ();
			}

			var building = town.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, town.TotalBuilders);
			Assert.AreEqual (1, town.Buildings.TotalCompleted);
			Assert.AreEqual (1, town.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, town.Buildings.TotalIncompleteHouses);*/

		}

		/*[Test]
		public void Test_Housing_2pop()
		{
			var settings = new EngineSettings (10);
			var buildActivity = new BuildActivity (settings, new EngineClock(settings));
			var town = new Town (2);

			foreach (var person in town.People) {
				person.ActivityType = ActivityType.Builder;
				buildActivity.Act ();
			}

			Assert.AreEqual (2, town.TotalActive);

			for (int i = 0; i < 100; i++) {
				foreach (var person in town.People) {
					buildActivity.Act ();
				}
			}

			var building = town.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, town.TotalBuilders);
			Assert.AreEqual (2, town.Buildings.TotalCompleted);
			Assert.AreEqual (2, town.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, town.Buildings.TotalIncompleteHouses);
		}*/

		//[Test]
		public void Test_Housing_5pop()
		{

			throw new NotImplementedException ();
			/*var settings = new EngineSettings (10);
			var buildActivity = new BuildActivity (settings, new EngineClock(settings));

			var town = new Town (5);

			foreach (var person in town.People) {
				person.ActivityType = ActivityType.Builder;
			
				buildActivity.Act ();
			}

			Assert.AreEqual (5, town.TotalActive);

			for (int i = 0; i < 1000; i++) {
				foreach (var person in town.People) {
					buildActivity.Act ();
				}
			}

			var building = town.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, town.TotalBuilders);
			Assert.AreEqual (5, town.Buildings.TotalCompleted);
			Assert.AreEqual (5, town.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, town.Buildings.TotalIncompleteHouses);
*/

		}
	}
}

