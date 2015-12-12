using System;
using NUnit.Framework;
using townsim.Engine.Activities;
using townsim.Entities;

namespace townsim.Engine.Tests.Unit.Activities
{
	[TestFixture]
	public class BuildActivityTestFixture
	{
		[Test]
		public void Test_Build()
		{
			var person = new Person ();

			person.AddSupply (SupplyTypes.Timber, 50);

			var town = new Town (person);

			var settings = new EngineSettings (10);

			var clock = new EngineClock (settings);

			var activity = new BuildActivity (person, settings, clock);

			activity.Start ();

			for (int i = 0; i < 1000; i++) {
				activity.ExecuteSingleCycle ();
			}

			Assert.AreEqual (100, person.Home.PercentComplete);
		}
	}
}

