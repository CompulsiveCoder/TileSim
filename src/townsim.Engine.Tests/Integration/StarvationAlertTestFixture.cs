﻿using System;
using NUnit.Framework;
using townsim.Entities;

namespace townsim.Engine.Tests.Integration
{
	[TestFixture]
	public class StarvationAlertTestFixture
	{
		[Test]
		public void Test_StarvationAlert()
		{
			throw new NotImplementedException ();
			/*var town = new Town (10);
			town.FoodSources = 5;

			var engine = new HungerEngine (new EngineSettings());

			foreach (var person in town.People) {
				engine.Update (person);
			}

			Assert.AreEqual (1, town.Alerts.Length);*/
		}
	}
}

