using System;
using townsim.Engine.Entities;
using NUnit.Framework;
using townsim.Engine.Needs;

namespace townsim.Engine.Tests.Unit.Needs
{
    [TestFixture(Category="Unit")]
    public class MealNeedIdentifierUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_RegisterIfNeeded_MealIsNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

            var person = new Person (settings);
            person.Vitals[PersonVital.Hunger] = 80;

            var mealNeed = new MealNeedIdentifier (settings);

            mealNeed.RegisterIfNeeded (person);

            Assert.AreEqual (1, person.Needs.Count);

            var need = person.Needs [0];

            Assert.AreEqual (ActionType.Eat, need.ActionType);
            Assert.AreEqual (ItemType.Food, need.ItemType);
            Assert.AreEqual (settings.DefaultEatAmount, need.Quantity);
            Assert.AreEqual (settings.DefaultPriorities[ItemType.Food], need.Priority);
        }

        [Test]
        public void Test_RegisterIfNeeded_MealNotNeeded()
        {
            var settings = EngineSettings.DefaultVerbose;

            var person = new Person (settings);

            var waterNeed = new MealNeedIdentifier (settings);

            waterNeed.RegisterIfNeeded (person);

            Assert.AreEqual (0, person.Needs.Count);
        }
    }
}

