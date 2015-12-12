using System;
using NUnit.Framework;
using townsim.Entities;
using townsim.Engine.Decisions;

namespace townsim.Engine.Tests.Unit.Decisions
{
	[TestFixture]
	public class TimberDecisionTestFixture
	{
		[Test]
		public void Test_Decide_HasDemandForTimber_DoesHaveEnoughWood()
		{
			var person = new Person ();

			var settings = new EngineSettings ();

			var timberNeeded = 50;

			var woodNeeded = timberNeeded * settings.TimberWasteRate;

			person.AddSupply (SupplyTypes.Wood, woodNeeded);
			person.AddDemand (SupplyTypes.Timber, timberNeeded);

			var decider = new TimberDecision (settings);

			decider.Decide (person);

			Assert.AreEqual(ActivityType.MillTimber, person.ActivityType);
		}

		[Test]
		public void Test_Decide_HasDemandForTimber_DoesNotHaveEnoughWood()
		{
			var person = new Person ();

			var settings = new EngineSettings ();

			var timberNeeded = 50;

			var woodNeeded = timberNeeded * settings.TimberWasteRate;

			person.AddDemand (SupplyTypes.Timber, timberNeeded);

			var decider = new TimberDecision (settings);

			decider.Decide (person);

			var demandForWoodHasBeenCreated = person.HasDemand (SupplyTypes.Wood);
			var amountOfWoodInDemand = person.GetDemandAmount(SupplyTypes.Wood);

			Assert.IsTrue (demandForWoodHasBeenCreated);

			Assert.AreEqual (woodNeeded, amountOfWoodInDemand);
		}
	}
}

