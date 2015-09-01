using System;
using NUnit.Framework;
using townsim.Entities;

namespace townsim.Engine.Tests
{
	[TestFixture]
	public class StarvationAlertTestFixture
	{
		[Test]
		public void Test_StarvationAlert()
		{
			var town = new Town (10);
			town.FoodSources = 5;

			var engine = new FoodEngine ();

			engine.Update (town);

			Assert.AreEqual (1, town.Alerts.Length);
		}
	}
}

