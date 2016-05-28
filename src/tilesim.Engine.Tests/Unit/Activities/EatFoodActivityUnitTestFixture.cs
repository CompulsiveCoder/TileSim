using System;
using NUnit.Framework;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class EatFoodActivityUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_EatFood_FoodAvailable()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            var settings = context.Settings;

            var person = new Person (settings);
            person.Inventory [ItemType.Food] = 100;
            person.Vitals[PersonVitalType.Hunger] = 80;

            var needEntry = new NeedEntry (ActivityVerb.Eat, ItemType.Food, PersonVitalType.NotSet, settings.DefaultEatAmount, settings.DefaultItemPriorities[ItemType.Food]);

            var activity = new EatFoodActivity (person, needEntry, settings, context.Console);

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            activity.Act (person);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual(75, person.Vitals[PersonVitalType.Hunger]);

        }
    }
}

