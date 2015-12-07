using System;
using NUnit.Framework;
using townsim.Entities;
using townsim.Engine.Effects;

namespace townsim.Engine.Tests.Integration
{
	[TestFixture]
	public class DehydrationAlertTestFixture
	{
		[Test]
		public void Test_DehydrationAlert()
		{
			var town = new Town (10);
			town.WaterSources = 5;

			var thirstEngine = new ThirstEffect (new EngineSettings(1));

			throw new NotImplementedException ();
			//thirstEngine.Update (town);

			//Assert.AreEqual (1, town.Alerts.Length);
		}
	}
}

