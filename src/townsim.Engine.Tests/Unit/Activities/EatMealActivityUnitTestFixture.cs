using System;
using NUnit.Framework;
using townsim.Engine.Activities;
using townsim.Engine.Entities;

namespace townsim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class EatMealActivityUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_EatMeal_FoodAvailable()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            // TODO: Remove if not needed. Shouldnt be needed in this unit test
           // context.World.Logic.AddActivity (typeof(EatMealActivity));

            var settings = context.Settings;

            settings.IsVerbose = true;

            var person = new Person (settings);
            person.Inventory [ItemType.Food] = 100;
            person.Vitals[PersonVital.Hunger] = 80;

            var needEntry = new NeedEntry (ItemType.Food, settings.DefaultEatAmount, settings.DefaultPriorities[ItemType.Food]);

            var activity = new EatMealActivity (person, needEntry, settings);

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            activity.Act (person);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual(70, person.Vitals[PersonVital.Hunger]);

        }
    }
}

