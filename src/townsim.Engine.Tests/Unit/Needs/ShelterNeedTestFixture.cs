using System;
using townsim.Engine.Entities;
using NUnit.Framework;
using townsim.Engine.Needs;

namespace townsim.Engine.Tests.Unit.Needs
{
	[TestFixture]
	public class ShelterNeedTestFixture
	{
		[Test]
		public void Test_RegisterIfNeeded_ShelterIsNeeded()
		{
			var person = new Person ();

			var shelterNeed = new ShelterNeedIdentifier (EngineSettings.DefaultVerbose);

			shelterNeed.RegisterIfNeeded (person);

			Assert.AreEqual (1, person.Needs.Count);

			var need = person.Needs [0];

			Assert.AreEqual (ItemType.Shelter, need.Type);
			Assert.AreEqual (1, need.Quantity);
			Assert.AreEqual (100, need.Priority);
		}

		[Test]
		public void Test_RegisterIfNeeded_ShelterNotNeeded()
		{
			var person = new Person ();

			// TODO: Should there be a helper function somewhere for creating a completed home?
			person.Home = new Building (BuildingType.House);
			person.Home.PercentComplete = 100;

			var shelterNeed = new ShelterNeedIdentifier (EngineSettings.DefaultVerbose);

			shelterNeed.RegisterIfNeeded (person);

			Assert.AreEqual (0, person.Needs.Count);
		}
	}
}

