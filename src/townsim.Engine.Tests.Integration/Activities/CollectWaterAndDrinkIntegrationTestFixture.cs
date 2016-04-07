﻿using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using townsim.Engine.Needs;
using townsim.Engine.Activities;
using townsim.Engine.Effects;

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
            context.Settings.DefaultDrinkAmount = 20; // Increase the amount the person drinks so the test goes faster
            context.Settings.WaterForThirstRatio = 10;

            // Disabled otherwise the thirst vital will never remain at zero
            //context.World.Logic.AddEffect (new ThirstEffect(context.Settings, context.Console));

            context.World.Logic.AddNeed (new DrinkWaterNeedIdentifier (context.Settings, context.Console));

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

            context.Initialize (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

            context.Run (10);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual (0, person.Vitals[PersonVital.Thirst]);
        }
    }
}

