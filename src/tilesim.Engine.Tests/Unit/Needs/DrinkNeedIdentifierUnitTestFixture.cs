using System;
using tilesim.Engine.Entities;
using NUnit.Framework;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Tests.Unit.Needs
{
    [TestFixture(Category="Unit")]
    public class DrinkNeedIdentifierUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_RegisterIfNeeded_WaterIsNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

            var person = new Person (settings);
            person.Vitals[PersonVitalType.Thirst] = 80;

            var waterNeed = new DrinkWaterNeedIdentifier (settings, new ConsoleHelper(settings));

            waterNeed.RegisterIfNeeded (person);

            Assert.AreEqual (1, person.Needs.Count);

            var need = person.Needs [0];

            Assert.AreEqual (ActivityType.Drink, need.ActionType);
            Assert.AreEqual (ItemType.Water, need.ItemType);
            Assert.AreEqual (settings.DefaultDrinkAmount, need.Quantity);
            Assert.AreEqual (settings.DefaultItemPriorities[ItemType.Water], need.Priority);
        }

        [Test]
        public void Test_RegisterIfNeeded_WaterNotNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

            var person = new Person (settings);

            var waterNeed = new DrinkWaterNeedIdentifier (settings, new ConsoleHelper(settings));

            waterNeed.RegisterIfNeeded (person);

            Assert.AreEqual (0, person.Needs.Count);
        }
    }
}

