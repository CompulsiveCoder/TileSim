using System;
using NUnit.Framework;
using townsim.Entities;
using townsim.Engine.Activities;
using townsim.Data.Tests;

namespace townsim.Engine.Tests.Unit.Activities
{
	[TestFixture]
	public class FellWoodActivityTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Act_1step_HasDemandForWood_DoesHaveEnoughTrees()
		{
			var person = new Person ();
			CurrentEngine.PlayerId = person.Id;

			var town = new Town (person);

			var settings = new EngineSettings ();

			var woodNeeded = 150;

			person.AddDemand (SupplyTypes.Wood, woodNeeded);

			var activity = new FellWoodActivity (person, settings);

			person.Start (ActivityType.FellWood, activity);

			activity.ExecuteSingleCycle ();

			Assert.IsNotNull (person.ActivityTarget);

			Assert.AreNotEqual (0, ((Plant)person.ActivityTarget).PercentHarvested);
		}

		[Test]
		public void Test_Act_100steps_HasDemandForWood_DoesHaveEnoughTrees()
		{
			var person = new Person ();
			CurrentEngine.PlayerId = person.Id;

			var town = new Town (person);

			var settings = new EngineSettings ();

			var woodNeeded = 200;

			person.AddDemand (SupplyTypes.Wood, woodNeeded);

			var activity = new FellWoodActivity (person, settings);

			person.Start (ActivityType.FellWood, activity);

			for (int i = 0; i < 100; i++) {
				activity.ExecuteSingleCycle ();
			}

			Assert.IsNull (person.ActivityTarget);

			Assert.AreEqual (woodNeeded, person.Supplies [SupplyTypes.Wood]);
		}
	}
}

