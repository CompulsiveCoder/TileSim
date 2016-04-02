using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Engine.Needs;
using townsim.Engine.Activities;

namespace townsim.Engine.Tests.Integration
{
	[TestFixture]
	public class BuildShelterIntegrationTestFixture
	{
		[Test]
		public void Test_DecideAndBuildShelter()
		{

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

			var person = new PeopleCreator ().CreateAdult(); // TODO: Store the PeopleCreator object somewhere else

			tile.AddPerson (person);
			tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (10)); // TODO: Should this PlantCreator object be stored somewhere better?

			// TODO: Remove if not needed
			//context.Settings.ConstructionRate = 10;
			context.Settings.FellingRate = 50;
			//context.Populate ();

			//var person = context.World.People [0];

			context.Start ();

			context.RunCycles (20);

			Assert.IsNotNull (person.Home);
			Assert.AreEqual (100, person.Home.PercentComplete);
            Assert.AreEqual (null, person.Activity);

		/*	var person = new Person ();

			//person.Decisions.Add (new ShelterDecision ());

			//var personEngine = new PersonEngine ();

			throw new NotImplementedException ();
//			personEngine.Act (person);
			//person.Act*/
		}
	}
}

