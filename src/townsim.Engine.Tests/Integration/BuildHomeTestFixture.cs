using System;
using NUnit.Framework;
using townsim.Data.Tests;
using townsim.Entities;

namespace townsim.Engine.Tests.Integration
{
	[TestFixture]
	public class BuildHomeTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_BuildHouse_IncludingFellWoodAndMillTimber()
		{
			var person = new Person ();

			var town = new Town (person);

			var engine = new townsimEngine (person, town);

			engine.EnableConsoleSummary = false;

			for (int i = 0; i < 20; i++) {
				engine.RunCycle ();
			}

			Assert.IsNotNull (person.Home);
			Assert.AreEqual (100, person.Home.PercentComplete);
			Assert.AreEqual (ActivityType.Inactive, person.Activity);
		}
	}
}

