using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Engine.Needs;
using townsim.Engine.Activities;

namespace townsim.Engine.Tests.Integration
{
    [TestFixture(Category="Integration")]
    public class CollectWaterAndDrinkIntegrationTestFixture : BaseEngineIntegrationTestFixture
    {
        [Test]
        public void Test_GetThirstyCollectWaterAndDrink()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();
            context.Settings.IsVerbose = true;
            context.Data.IsVerbose = true;

            context.Settings.DefaultCollectWaterRate = 50; // Increase the rate of water collection so the test goes faster
            context.Settings.DefaultDrinkAmount = 100; // Increase the amount the person drinks so the test goes faster

            context.World.Logic.AddNeed (new DrinkNeedIdentifier (context.Settings));
            //context.World.Logic.AddDecision (new ShelterDecision ());
            context.World.Logic.AddActivity (typeof(CollectWaterActivity));
            context.World.Logic.AddActivity (typeof(DrinkWaterActivity));

            var tile = context.World.Tiles [0];

            tile.Inventory[ItemType.Water] = 200;

            var person = new PersonCreator (context.Settings).CreateAdult(); // TODO: Store the PersonCreator object somewhere else

            person.Vitals[PersonVital.Thirst] = 90;

            tile.AddPerson (person);

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            context.Start (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

            context.RunCycles (5);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual (0, person.Vitals[PersonVital.Thirst]);
        }
    }
}

