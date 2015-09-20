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

			var thirstEngine = new ThirstEngine (new EngineSettings(1));

			throw new NotImplementedException ();
			//thirstEngine.Update (town);

			//Assert.AreEqual (1, town.Alerts.Length);
		}
	}
}

