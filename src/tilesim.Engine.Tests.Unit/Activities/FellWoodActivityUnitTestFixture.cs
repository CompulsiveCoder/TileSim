using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;
using tilesim.Tests;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class FellWoodActivityUnitTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Act_StartFelling()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

			var context = MockEngineContext.New ();

            var settings = EngineSettings.DefaultVerbose;

            // TODO: Streamline the process of adding people and trees
			var person = new PersonCreator (settings).CreateAdult ();

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (5));

            foreach (var tree in tile.Trees)
                tree.Height = 10;

            var needEntry = new NeedEntry (ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet, 50, 101);

            var activity = new FellWoodActivity (person, needEntry, settings, new ConsoleHelper(settings));

			Console.WriteLine ("");
			Console.WriteLine ("Executing target");
			Console.WriteLine ("");

			activity.Act (person);

            Assert.IsNotNull (activity.TreesToFell);
            Assert.AreEqual (5, activity.TreesToFell.Length);
            // TODO: Reimplement. This functionality isn't full implemented
            //Assert.AreEqual (0.1m, activity.PercentComplete);
		}

		[Test]
		public void Test_Act_ContinueFelling()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

			var context = MockEngineContext.New ();

            var settings = EngineSettings.DefaultVerbose;
            settings.TimberFellingRate = 10;

			var person = new PersonCreator (settings).CreateAdult ();

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (3));

            foreach (var tree in tile.Trees)
                tree.Height = 10;

            var needEntry = new NeedEntry (ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet, 50, 101);

            var activity = new FellWoodActivity (person, needEntry, settings, new ConsoleHelper(settings));
            activity.TreesToFell = new Plant[]{tile.Trees [0],tile.Trees [1],tile.Trees [2]};

            Console.WriteLine ("");
            Console.WriteLine ("Executing target");
            Console.WriteLine ("");

			activity.Act (person);

            // TODO: Implement check. Percentage calculation not yet working.
            //Assert.AreEqual (5, activity.PercentComplete);
            Assert.AreEqual(10, activity.TreesToFell[0].PercentHarvested);
		}

		[Test]
		public void Test_Act_FinishedFelling()
        {
            Console.WriteLine ("");
            Console.WriteLine("Preparing test");
            Console.WriteLine("");

			var context = MockEngineContext.New ();

            var settings = EngineSettings.DefaultVerbose;
            settings.TimberFellingRate = 10;

			var person = new PersonCreator (settings).CreateAdult ();

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (2));

            foreach (var tree in tile.Trees)
                tree.Height = 10;

            var needEntry = new NeedEntry (ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet, 50, 101);

			person.AddNeed (needEntry);

            var activity = new FellWoodActivity (person, needEntry, settings, new ConsoleHelper(settings));
            activity.TreesToFell = new Plant[]{tile.Trees [0]};
			activity.TreesToFell[0].PercentHarvested = 99;
			activity.TotalWoodFelled = 40; // Add just enough so the activity can finish
            activity.IncreasePercentComplete(99);

            var totalWoodExpected = activity.TreesToFell[0].Height;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

 			activity.Act (person);

            Assert.IsTrue (activity.IsFinished);

            Assert.AreEqual (totalWoodExpected, person.Inventory.Items [ItemType.Wood]);
            Assert.AreEqual (0, activity.TreesToFell.Length);


			Assert.AreEqual (0, person.Needs.Count);
		}
	}
}

