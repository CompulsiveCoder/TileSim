using System;
using NUnit.Framework;
using townsim.Engine.Activities;
using townsim.Engine.Entities;

namespace townsim.Engine.Tests.Unit.Activities
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

            settings.IsVerbose = true;

            var person = new Person (settings);
            person.Inventory [ItemType.Food] = 100;
            person.Vitals[PersonVital.Hunger] = 80;

            var needEntry = new NeedEntry (ActionType.Eat, ItemType.Food, settings.DefaultEatAmount, settings.DefaultPriorities[ItemType.Food]);

            var activity = new EatFoodActivity (person, needEntry, settings, context.Console);

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

