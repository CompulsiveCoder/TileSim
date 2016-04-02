using System;
using townsim.Engine.Entities;
using NUnit.Framework;
using townsim.Engine.Needs;

namespace townsim.Engine.Tests.Unit.Needs
{
    [TestFixture(Category="Unit")]
    public class WaterNeedIdentifierUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_RegisterIfNeeded_WaterIsNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

            var person = new Person (settings);
            person.Vitals[PersonVital.Thirst] = 80;

            var waterNeed = new DrinkNeedIdentifier (settings);

            waterNeed.RegisterIfNeeded (person);

            Assert.AreEqual (1, person.Needs.Count);

            var need = person.Needs [0];

            Assert.AreEqual (ItemType.Drink, need.Type);
            Assert.AreEqual (settings.DefaultDrinkAmount, need.Quantity);
            Assert.AreEqual (settings.DefaultPriorities[ItemType.Water], need.Priority);
        }

        [Test]
        public void Test_RegisterIfNeeded_WaterNotNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

            var person = new Person (settings);

            var waterNeed = new DrinkNeedIdentifier (settings);

            waterNeed.RegisterIfNeeded (person);

            Assert.AreEqual (0, person.Needs.Count);
        }
    }
}

