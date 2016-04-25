using System;
using NUnit.Framework;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class CollectWaterActivityUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_CollectWater_WaterAvailable()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New (EngineSettings.DefaultVerbose);

            context.World.Logic.AddActivity (typeof(CollectWaterActivity));

            var settings = context.Settings;

            var tile = context.World.Tiles [0];

            tile.Inventory [ItemType.Water] = 100;

            var person = new Person (settings);
            person.Tile = tile;

            var needEntry = new NeedEntry (ActionType.Gather, ItemType.Water, settings.DefaultDrinkAmount, settings.DefaultPriorities[ItemType.Water]);

            var activity = new CollectWaterActivity (person, needEntry, settings, new ConsoleHelper(settings));

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            activity.Act (person);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual(10, person.Inventory.Items[ItemType.Water]);

        }
    }
}

