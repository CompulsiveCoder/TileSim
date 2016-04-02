using System;
using NUnit.Framework;
using townsim.Engine.Activities;
using townsim.Entities;

namespace townsim.Engine.Tests.Unit.Activities
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class BuildActivityTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Build_PersonHasEnoughTimber()
		{
			throw new NotImplementedException ();
			/*// Create the game context
			var context = EngineContext.New ();

			context.Settings.GameSpeed = 10;
			context.Settings.DefaultTownPopulation = 1;
			context.Settings.ConstructionRate = 10;

			context.Populate ();

			var person = context.World.People [0];

			var timberNeeded = context.Settings.TimberNeededForHouse;

			// Add enough timber for the house
			person.AddSupply (NeedType.Timber, timberNeeded);

			// Run the activity
			var activity = new BuildActivity (person, context);
			activity.Start ();
			activity.RunCycles (20);

			Assert.AreEqual (100, person.Home.PercentComplete);*/
		}
	}
}

