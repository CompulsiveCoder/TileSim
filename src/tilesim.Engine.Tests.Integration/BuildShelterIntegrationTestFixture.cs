using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Tests.Integration
{
    [TestFixture(Category="Integration")]
	public class BuildShelterIntegrationTestFixture : BaseEngineIntegrationTestFixture
	{
		[Test]
		public void Test_DecideAndBuildShelter()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

			var context = MockEngineContext.New ();
			context.Data.IsVerbose = true;

            context.Settings.MinimumTreeSize = 90; // Increase the size of the trees to speed up test
			context.Settings.WoodRequiredForTimber = 1.1m; // Reduce the waste rate to increase the speed of the test
            context.Settings.ConstructionRate = 50; // Increase construction rate to speed up test
            context.Settings.TimberMillingRate = 50;
            context.Settings.FellingRate = 50;

            context.World.Logic.AddNeed (new BuildShelterNeedIdentifier (context.Settings, context.Console));
			//context.World.Logic.AddDecision (new ShelterDecision ());
			context.World.Logic.AddActivity (typeof(BuildShelterActivity));
			context.World.Logic.AddActivity (typeof(MillTimberActivity));
			context.World.Logic.AddActivity (typeof(FellWoodActivity));

			var tile = context.World.Tiles [0];

            var person = new PersonCreator (context.Settings).CreateAdult(); // TODO: Store the PersonCreator object somewhere else

			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (10)); // TODO: Should this PlantCreator object be stored somewhere better?

            context.Player = person;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

			context.Initialize (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

			context.Run (20);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

			Assert.IsNotNull (person.Home);
			Assert.AreEqual (100, person.Home.PercentComplete);
            Assert.AreEqual (null, person.Activity);
		}
	}
}

