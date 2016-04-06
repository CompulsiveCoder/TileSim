using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Engine.Needs;
using townsim.Engine.Activities;

namespace townsim.Engine.Tests.Integration
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
            context.Settings.IsVerbose = true;
            context.Data.IsVerbose = true;

            context.Settings.DefaultGatherFoodRate = 50; // Increase the rate of food gathering so the test goes faster
            context.Settings.DefaultEatAmount = 100; // Increase the amount the person eats so the test goes faster

            context.World.Logic.AddNeed (new MealNeedIdentifier (context.Settings));
            //context.World.Logic.AddDecision (new ShelterDecision ());
            context.World.Logic.AddActivity (typeof(GatherFoodActivity));
            context.World.Logic.AddActivity (typeof(EatFoodActivity));

            var tile = context.World.Tiles [0];

            tile.Inventory[ItemType.Food] = 200;

            var person = new PersonCreator (context.Settings).CreateAdult(); // TODO: Store the PersonCreator object somewhere else

            person.Vitals[PersonVital.Hunger] = 90;

            tile.AddPerson (person);

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            context.Initialize (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

            context.Run (5);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual (0, person.Vitals[PersonVital.Hunger]);
        }
    }
}

