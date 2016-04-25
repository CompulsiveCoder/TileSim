using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
	public class BuildShelterActivityUnitTestFixture
	{
		[Test]
		public void Test_Build_StartConstruction()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var settings = EngineSettings.DefaultVerbose;

			var person = new Person (settings);
            person.Inventory.AddItem (ItemType.Timber, 50); // TODO: Get the 50 value from somewhere easier to configures

            var needEntry = new NeedEntry (ActionType.Build, ItemType.Shelter, 1, 100);

            var activity = new BuildShelterActivity (person, needEntry, settings, new ConsoleHelper(settings));

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

			activity.Act (person);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

			Assert.IsNotNull (person.Home);
            Assert.AreEqual (50, person.Home.Inventory.Items[ItemType.Timber]); // TODO: Should all the timber necessarily be provided as soon as construction starts?
		}

		[Test]
		public void Test_Build_ContinueConstruction()
        {
            var settings = EngineSettings.DefaultVerbose;
            settings.ConstructionRate = 1;

			var person = new Person (settings);
			person.Home = new Building (BuildingType.House, settings);
            person.Home.Inventory.Items[ItemType.Timber] = 50; // TODO: Get the 50 value from somewhere easier to configures

            var needEntry = new NeedEntry (ActionType.Build, ItemType.Shelter, 1, 100);

            var activity = new BuildShelterActivity (person, needEntry, settings, new ConsoleHelper(settings));

			activity.Act (person);

			Assert.AreEqual (1, person.Home.PercentComplete);
		}


		[Test]
		public void Test_Build_CompleteConstruction()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var settings = EngineSettings.DefaultVerbose;
            settings.ConstructionRate = 10;

			var person = new Person (settings);
            person.Home = new Building (BuildingType.Shelter, settings);
            person.Home.Inventory.Items [ItemType.Timber] = settings.ShelterTimberCost;
			person.Home.PercentComplete = 99;

            var needEntry = new NeedEntry (ActionType.Build, ItemType.Shelter, 1, 100);

			person.AddNeed (needEntry);

            var activity = new BuildShelterActivity (person, needEntry, settings, new ConsoleHelper(settings));
            activity.Shelter = person.Home;

			activity.Act (person);

			Assert.AreEqual (100, person.Home.PercentComplete);
			Assert.IsTrue (person.Home.IsCompleted);

			Assert.AreEqual (0, person.Needs.Count);
		}

		[Test]
		public void Test_Build_NotEnoughTimber()
        {
            var settings = EngineSettings.DefaultVerbose;

			var person = new Person (settings);

            var needEntry = new NeedEntry (ActionType.Build, ItemType.Shelter, 1, 100);

            var activity = new BuildShelterActivity (person, needEntry, settings, new ConsoleHelper(settings));

			activity.Act (person);

			Assert.AreEqual (1, person.Needs.Count);

			var foundNeedEntry = person.Needs [0];

			Assert.AreEqual (ItemType.Timber, foundNeedEntry.ItemType);
			Assert.AreEqual (50, foundNeedEntry.Quantity);
			Assert.AreEqual (101, foundNeedEntry.Priority);
		}
	}
}

