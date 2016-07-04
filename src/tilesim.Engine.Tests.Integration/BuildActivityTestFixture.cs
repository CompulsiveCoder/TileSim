using System;
using NUnit.Framework;
using tilesim.Data;
using tilesim.Data.Tests;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Tests.Integration.Activities
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

			var tile = new Tile (person);

			person.Tile = tile;

			person.ActivityType = ActivityType.Builder;
			buildActivity.Act ();

			Assert.AreEqual (1, tile.TotalActive);

			for (int i = 0; i < 100; i++) {
				buildActivity.Act ();
			}

			var building = tile.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, tile.TotalBuilders);
			Assert.AreEqual (1, tile.Buildings.TotalCompleted);
			Assert.AreEqual (1, tile.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, tile.Buildings.TotalIncompleteHouses);*/

		}

		/*[Test]
		public void Test_Housing_2pop()
		{
			var settings = new EngineSettings (10);
			var buildActivity = new BuildActivity (settings, new EngineClock(settings));
			var tile = new Tile (2);

			foreach (var person in tile.People) {
				person.ActivityType = ActivityType.Builder;
				buildActivity.Act ();
			}

			Assert.AreEqual (2, tile.TotalActive);

			for (int i = 0; i < 100; i++) {
				foreach (var person in tile.People) {
					buildActivity.Act ();
				}
			}

			var building = tile.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, tile.TotalBuilders);
			Assert.AreEqual (2, tile.Buildings.TotalCompleted);
			Assert.AreEqual (2, tile.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, tile.Buildings.TotalIncompleteHouses);
		}*/

		//[Test]
		public void Test_Housing_5pop()
		{

			throw new NotImplementedException ();
			/*var settings = new EngineSettings (10);
			var buildActivity = new BuildActivity (settings, new EngineClock(settings));

			var tile = new Tile (5);

			foreach (var person in tile.People) {
				person.ActivityType = ActivityType.Builder;
			
				buildActivity.Act ();
			}

			Assert.AreEqual (5, tile.TotalActive);

			for (int i = 0; i < 1000; i++) {
				foreach (var person in tile.People) {
					buildActivity.Act ();
				}
			}

			var building = tile.Buildings [0];

			Assert.AreEqual (100, building.PercentComplete);
			Assert.AreEqual (0, tile.TotalBuilders);
			Assert.AreEqual (5, tile.Buildings.TotalCompleted);
			Assert.AreEqual (5, tile.Buildings.TotalCompletedHouses);
			Assert.AreEqual (0, tile.Buildings.TotalIncompleteHouses);
*/

		}
	}
}

