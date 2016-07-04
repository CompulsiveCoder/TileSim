using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Decisions;

namespace tilesim.Engine.Tests.Unit.Decisions
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class TimberDecisionTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Decide_HasDemandForTimber_DoesHaveEnoughWood()
		{
			throw new NotImplementedException ();
			/*var context = EngineContext.New();
			context.Settings.DefaultTilePopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			var timberNeeded = 50;

			var woodNeeded = timberNeeded * context.Settings.TimberWasteRate;

			person.AddSupply (NeedType.Wood, woodNeeded);
			person.AddDemand (NeedType.Timber, timberNeeded);

			var decider = new TimberDecision (context);

			decider.Decide (person);

			Assert.AreEqual(ActivityType.MillTimber, person.ActivityType);*/
		}

		// TODO: Overhaul and re-enable
		//[Test]s
		public void Test_Decide_HasDemandForTimber_DoesNotHaveEnoughWood()
		{
			throw new NotImplementedException ();
			/*var context = EngineContext.New();
			context.Settings.DefaultTilePopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			var timberNeeded = 50;

			var woodNeeded = timberNeeded * context.Settings.TimberWasteRate;

			person.AddDemand (NeedType.Timber, timberNeeded);

			var decider = new TimberDecision (context);

			decider.Decide (person);

			var demandForWoodHasBeenCreated = person.HasDemand (NeedType.Wood);
			var amountOfWoodInDemand = person.GetDemandAmount(NeedType.Wood);

			Assert.IsTrue (demandForWoodHasBeenCreated);

			Assert.AreEqual (woodNeeded, amountOfWoodInDemand);*/
		}
	}
}

