using System;
using NUnit.Framework;
using townsim.Entities;

namespace townsim.Engine.Tests
{
	[TestFixture]
	public class DehydrationAlertTestFixture
	{
		[Test]
		public void Test_DehydrationAlert()
		{
			var town = new Town (10);
			town.WaterSources = 5;

			var waterSourcesEngine = new WaterSourcesEngine ();

			waterSourcesEngine.Update (town);

			Assert.AreEqual (1, town.Alerts.Length);
		}
	}
}

