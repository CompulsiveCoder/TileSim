using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Tests.Integration
{
    [TestFixture(Category="Integration")]
    public class CollectFoodAndEatIntegrationTestFixture : BaseEngineIntegrationTestFixture
    {
        [Test]
        public void Test_GetHungryCollectFoodAndEat()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            context.Settings.DefaultGatherFoodRate = 50; // Increase the rate of food gathering so the test goes faster
            context.Settings.DefaultEatAmount = 100; // Increase the amount the person eats so the test goes faster

            context.World.Logic.AddNeed (new EatFoodNeedIdentifier (context.Settings, context.Console));
            context.World.Logic.AddActivity (typeof(GatherFoodActivity));
            context.World.Logic.AddActivity (typeof(EatFoodActivity));

            var tile = context.World.Tiles [0];

            tile.Inventory[ItemType.Food] = 200;

            var person = new PersonCreator (context.Settings).CreateAdult(); // TODO: Store the PersonCreator object somewhere else

            person.Vitals[PersonVitalType.Hunger] = 90;

            tile.AddPerson (person);

            context.Player = person;

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            context.Initialize (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

            context.Run (5);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual (40, person.Vitals[PersonVitalType.Hunger]);
        }
    }
}

