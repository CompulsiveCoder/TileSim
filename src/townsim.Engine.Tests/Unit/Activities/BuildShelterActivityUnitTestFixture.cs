using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Engine.Activities;
using townsim.Engine.Needs;

namespace townsim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
	public class BuildShelterActivityUnitTestFixture
	{
		[Test]
		public void Test_Build_StartConstruction()
		{
			var person = new Person ();
			person.AddSupply (ItemType.Timber, 50); // TODO: Get the 50 value from somewhere easier to configures

			var needEntry = new NeedEntry (ItemType.Shelter, 1, 100);

			var activity = new BuildShelterActivity (person, needEntry, EngineSettings.DefaultVerbose);

			activity.Act (person);

			Assert.IsNotNull (person.Home);
			Assert.AreEqual (50, person.Home.Timber); // TODO: Should all the timber necessarily be provided as soon as construction starts?
		}

		[Test]
		public void Test_Build_ContinueConstruction()
		{
			var person = new Person ();
			person.Home = new Building (BuildingType.House);
			person.Home.Timber = 50; // TODO: Get the 50 value from somewhere easier to configures

			var needEntry = new NeedEntry (ItemType.Shelter, 1, 100);

			var settings = EngineSettings.DefaultVerbose;
			settings.ConstructionRate = 1;

			var activity = new BuildShelterActivity (person, needEntry, settings);

			activity.Act (person);

			Assert.AreEqual (1, person.Home.PercentComplete);
		}


		[Test]
		public void Test_Build_CompleteConstruction()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

			var person = new Person ();
            person.Home = new Building (BuildingType.Shelter);
			person.Home.Timber = 50; // TODO: Get the 50 value from somewhere easier to configures
			person.Home.PercentComplete = 99.9;

			var needEntry = new NeedEntry (ItemType.Shelter, 1, 100);

			person.AddNeed (needEntry);

			var activity = new BuildShelterActivity (person, needEntry, EngineSettings.DefaultVerbose);
            activity.Shelter = person.Home;

			activity.Act (person);

			Assert.AreEqual (100, person.Home.PercentComplete);
			Assert.IsTrue (person.Home.IsCompleted);

			Assert.AreEqual (0, person.Needs.Count);
		}

		[Test]
		public void Test_Build_NotEnoughTimber()
		{
			var person = new Person ();

			var needEntry = new NeedEntry (ItemType.Shelter, 1, 100);

			var activity = new BuildShelterActivity (person, needEntry, EngineSettings.DefaultVerbose);

			activity.Act (person);

			Assert.AreEqual (1, person.Needs.Count);

			var foundNeedEntry = person.Needs [0];

			Assert.AreEqual (ItemType.Timber, foundNeedEntry.Type);
			Assert.AreEqual (50, foundNeedEntry.Quantity);
			Assert.AreEqual (101, foundNeedEntry.Priority);
		}
	}
}

