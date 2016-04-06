﻿using System;
using NUnit.Framework;
using townsim.Engine.Activities;
using townsim.Engine.Entities;

namespace townsim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class GatherFoodActivityUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_GatherFood_FoodAvailable()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            // TODO: Remove if not needed
            //context.World.Logic.AddActivity (typeof(GatherFoodActivity));

            var settings = context.Settings;

            settings.IsVerbose = true;

            var tile = context.World.Tiles [0];

            tile.Inventory [ItemType.Food] = 100;

            var person = new Person (settings);
            person.Tile = tile;

            var needEntry = new NeedEntry (ActionType.Gather, ItemType.Food, settings.DefaultEatAmount, settings.DefaultPriorities[ItemType.Food]);

            var activity = new GatherFoodActivity (person, needEntry, settings);

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            activity.Act (person);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual(10, person.Inventory.Items[ItemType.Food]);

        }
    }
}

