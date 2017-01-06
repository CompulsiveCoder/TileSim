using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Tests.Integration
{
    [TestFixture]
    public class FellWoodIntegrationTestFixture : BaseEngineIntegrationTestFixture
    {
        [Test]
        public void Test_FellWood()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();
            //context.PopulateFromSettings ();

            context.Settings.TimberFellingRate = 20; // Increase the rate of food gathering so the test goes faster
            //context.Settings.DefaultEatAmount = 100; // Increase the amount the person eats so the test goes faster

            //context.World.Logic.AddNeed (new SleepNeedIdentifier (context.Settings, context.Console));
            context.World.Logic.AddActivity (typeof(FellWoodActivity));

            var tile = context.World.Tiles [0];

            var person = new PersonCreator (context.Settings).CreateAdult ();

            context.World.Tiles[0].AddPerson (person);

            context.Player = person;

            //var person = context.Player;
            person.AddNeed(new NeedEntry(ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet, 100, 100));
            person.Settings = PersonSettings.Autonomous;

            for (int i = 0; i < 10; i++) {
                var tree = new PlantCreator (context.Settings).CreateTree ();

                tree.Height = 10;

                tile.AddTrees (tree);
            }
            //person.Vitals[PersonVitalType.Energy] = 0;

            /*tile.AddPerson (person);

            context.Player = person;
*/
            Console.WriteLine ("");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("");

            context.Initialize (); // TODO: Should Start be part of the test? Or part of the preparation before the above console output?

            var numberOfCycles = 50;

            context.Run (numberOfCycles);

            Console.WriteLine ("");
            Console.WriteLine ("Analysing test");
            Console.WriteLine ("");

            Assert.AreEqual (100, person.Inventory[ItemType.Wood]);
        }
    }
}

