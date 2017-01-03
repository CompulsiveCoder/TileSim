using System;
using tilesim.Engine.Entities;
using NUnit.Framework;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Tests.Unit.Needs
{
    [TestFixture(Category="Unit")]
    public class ShelterNeedIdentifierUnitTestFixture : BaseEngineUnitTestFixture
	{
		[Test]
		public void Test_RegisterIfNeeded_ShelterIsNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

			var person = new Person (settings);

            var shelterNeed = new BuildShelterNeedIdentifier (settings, new ConsoleHelper(settings));

			shelterNeed.RegisterIfNeeded (person);

			Assert.AreEqual (1, person.Needs.Count);

			var need = person.Needs [0];

			Assert.AreEqual (ItemType.Shelter, need.ItemType);
			Assert.AreEqual (1, need.Quantity);
            Assert.AreEqual (settings.DefaultItemPriorities[ItemType.Shelter], need.Priority);
		}

		[Test]
		public void Test_RegisterIfNeeded_ShelterNotNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

			var person = new Person (settings);

			// TODO: Should there be a helper function somewhere for creating a completed home?
			person.Home = new Building (BuildingType.House, settings);
            person.Home.SetPercentComplete(100);

            var shelterNeed = new BuildShelterNeedIdentifier (settings, new ConsoleHelper(settings));

			shelterNeed.RegisterIfNeeded (person);

			Assert.AreEqual (0, person.Needs.Count);
		}
	}
}

