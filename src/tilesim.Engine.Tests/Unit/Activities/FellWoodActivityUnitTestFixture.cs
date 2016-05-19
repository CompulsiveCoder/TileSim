using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;
using tilesim.Data.Tests;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class FellWoodActivityUnitTestFixture : BaseDataTestFixture
	{
		[Test]
		public void Test_Act_StartFelling()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

			var context = MockEngineContext.New ();

            var settings = EngineSettings.DefaultVerbose;

			var person = new PersonCreator (settings).CreateAdult ();

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (2));

            var needEntry = new NeedEntry (ActivityType.Fell, ItemType.Wood, PersonVitalType.NotSet, 50, 101);

            var activity = new FellWoodActivity (person, needEntry, settings, new ConsoleHelper(settings));

			Console.WriteLine ("");
			Console.WriteLine ("Executing target");
			Console.WriteLine ("");

			activity.Act (person);

			Assert.IsNotNull (activity.Target);
		}

		[Test]
		public void Test_Act_ContinueFelling()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

			var context = MockEngineContext.New ();

            var settings = EngineSettings.DefaultVerbose;
            settings.FellingRate = 10;

			var person = new PersonCreator (settings).CreateAdult ();

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (2));

            var needEntry = new NeedEntry (ActivityType.Fell, ItemType.Wood, PersonVitalType.NotSet, 50, 101);

            var activity = new FellWoodActivity (person, needEntry, settings, new ConsoleHelper(settings));
			activity.Target = tile.Trees [0];

            Console.WriteLine ("");
            Console.WriteLine ("Executing target");
            Console.WriteLine ("");

			activity.Act (person);

			Assert.AreEqual(10, activity.Target.PercentHarvested);
		}

		[Test]
		public void Test_Act_FinishedFelling()
        {
            Console.WriteLine ("");
            Console.WriteLine("Preparing test");
            Console.WriteLine("");

			var context = MockEngineContext.New ();

            var settings = EngineSettings.DefaultVerbose;
            settings.FellingRate = 10;

			var person = new PersonCreator (settings).CreateAdult ();

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (2));

            var needEntry = new NeedEntry (ActivityType.Fell, ItemType.Wood, PersonVitalType.NotSet, 50, 101);

			person.AddNeed (needEntry);

            var activity = new FellWoodActivity (person, needEntry, settings, new ConsoleHelper(settings));
			activity.Target = tile.Trees [0];
			activity.Target.PercentHarvested = 100;
			activity.TotalWoodFelled = 40; // Add just enough so the activity can finish

			var totalWoodExpected = activity.Target.Size;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

			activity.Act (person);

			Assert.IsTrue (activity.IsFinished);
			Assert.IsNull(activity.Target);
            Assert.AreEqual (totalWoodExpected, person.Inventory.Items [ItemType.Wood]);

			Assert.AreEqual (0, person.Needs.Count);
		}

		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Act_1step_PersonHasDemandForWood_TileDoesHaveEnoughTrees()
		{
			throw new NotImplementedException ();
			/*var context = EngineContext.New();
			context.Settings.DefaultTilePopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			var woodNeeded = 150;

			person.AddDemand (NeedType.Wood, woodNeeded);

			var activity = new FellWoodActivity (person, context);

			activity.AssignPerson (person);

			activity.StartSingleCycle ();

			Assert.IsNotNull (person.Activity.Target);

			Assert.AreNotEqual (0, ((Plant)person.Activity.Target).PercentHarvested);*/
		}

		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Act_100steps_PersonHasDemandForWood_TileDoesHaveEnoughTrees()
		{
			throw new NotImplementedException ();

			/*var context = EngineContext.New();
			context.Settings.DefaultTilePopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			// Add demand for wood
			var woodNeeded = 200;
			person.AddDemand (NeedType.Wood, woodNeeded);

			// Create the fell wood activity
			var activity = new FellWoodActivity (person, context);

			activity.RunCycles (100);

			Assert.AreEqual (woodNeeded, person.Supplies [NeedType.Wood]);*/
		}
	}
}

