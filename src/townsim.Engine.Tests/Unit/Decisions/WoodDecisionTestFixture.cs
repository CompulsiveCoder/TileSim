using System;
using NUnit.Framework;
using townsim.Data.Tests;
using townsim.Engine.Entities;

namespace townsim.Engine.Tests.Unit.Decisions
{
	// TODO: Overhaul and re-enable
	//[TestFixture]
	public class WoodDecisionTestFixture : BaseDataTestFixture
	{
		// TODO: Overhaul and re-enable
		//[Test]
		public void Test_Decide_HasDemandForWood_DoesHaveEnoughTrees()
		{
			throw new NotImplementedException ();
			/*var context = EngineContext.New();
			context.Settings.DefaultTownPopulation = 1;
			context.Populate ();

			var person = context.World.People [0];

			var timberNeeded = context.Settings.TimberNeededForHouse;

			var woodNeeded = timberNeeded * context.Settings.TimberWasteRate;

			person.AddDemand (NeedType.Wood, woodNeeded);

			var decider = new WoodDecision (context);

			decider.Decide (person);

			Assert.AreEqual(ActivityType.FellWood, person.ActivityType);*/
		}
	}
}

