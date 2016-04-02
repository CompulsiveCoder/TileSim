using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Engine.Needs;
using townsim.Engine.Activities;

namespace townsim.Engine.Tests.Integration
{
    [TestFixture(Category="Integration")]
	public class BuildShelterIntegrationTestFixture
	{
		[Test]
		public void Test_DecideAndBuildShelter()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

			var context = EngineContext.New ();
			context.Settings.IsVerbose = true;
			context.Data.IsVerbose = true;

            context.Settings.MinimumTreeSize = 90; // Increase the size of the trees to speed up test
			context.Settings.WoodRequiredForTimber = 1.1m; // Reduce the waste rate to increase the speed of the test
            context.Settings.ConstructionRate = 50; // Increase construction rate to speed up test

			context.World.Logic.AddNeed (new ShelterNeedIdentifier (context.Settings));
			//context.World.Logic.AddDecision (new ShelterDecision ());
			context.World.Logic.AddActivity (typeof(BuildShelterActivity));
			context.World.Logic.AddActivity (typeof(MillTimberActivity));
			context.World.Logic.AddActivity (typeof(FellWoodActivity));

			var tile = context.World.Tiles [0];

            var person = new PersonCreator (context.Settings).CreateAdult(); // TODO: Store the PersonCreator object somewhere else

			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (10)); // TODO: Should this PlantCreator object be stored somewhere better?

			context.Settings.FellingRate = 50;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

			context.Start (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

			context.RunCycles (20);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

			Assert.IsNotNull (person.Home);
			Assert.AreEqual (100, person.Home.PercentComplete);
            Assert.AreEqual (null, person.Activity);
		}
	}
}

