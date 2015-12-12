using System;
using NUnit.Framework;
using townsim.Data.Tests;
using townsim.Entities;

namespace townsim.Engine.Tests.Unit.Decisions
{
	[TestFixture]
	public class WoodDecisionTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Decide_HasDemandForWood_DoesHaveEnoughTrees()
		{
			var person = new Person ();

			var town = new Town (person);

			var settings = new EngineSettings ();

			var timberNeeded = 50;

			var woodNeeded = timberNeeded * settings.TimberWasteRate;

			person.AddDemand (SupplyTypes.Wood, woodNeeded);

			var decider = new WoodDecision (settings);

			decider.Decide (person);

			Assert.AreEqual(ActivityType.FellWood, person.ActivityType);
		}
	}
}

