using System;
using NUnit.Framework;
using townsim.Engine.Activities;
using townsim.Engine.Entities;

namespace townsim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
    public class DrinkWaterActivityUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_DrinkWater_WaterAvailable()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            context.World.Logic.AddActivity (typeof(DrinkWaterActivity));

            var settings = context.Settings;

            settings.IsVerbose = true;

            //var tile = context.World.Tiles [0];

            //tile.Inventory [ItemType.Water] = 100;

            var person = new Person (settings);
            person.Inventory [ItemType.Water] = 100;
            person.Vitals[PersonVital.Thirst] = 80;
            //person.Tile = tile;

            var needEntry = new NeedEntry (ItemType.Water, settings.DefaultDrinkAmount, settings.DefaultPriorities[ItemType.Water]);

            var activity = new DrinkWaterActivity (person, needEntry, settings);

            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            activity.Act (person);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual(70, person.Vitals[PersonVital.Thirst]);

        }
    }
}

