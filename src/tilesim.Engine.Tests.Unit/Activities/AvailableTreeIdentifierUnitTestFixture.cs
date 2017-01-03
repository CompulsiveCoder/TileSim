using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture]
    public class AvailableTreeIdentifierUnitTestFixture : BaseEngineUnitTestFixture
    {
        [Test]
        public void Test_IdentifyAvailableTrees()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("");

            var context = MockEngineContext.New ();

            var settings = EngineSettings.DefaultVerbose;

            var treesFound = new Plant[]{ };

            // TODO: Streamline the process of adding people and trees
            var person = new PersonCreator (settings).CreateAdult ();

            var tile = context.World.Tiles[0];
            tile.AddPerson (person);
            tile.AddTrees (new PlantCreator (context.Settings).CreateTrees (2));

            foreach (var tree in tile.Trees)
                tree.Height = 25;

            var needEntry = new NeedEntry (ActivityVerb.Fell, ItemType.Wood, PersonVitalType.NotSet, 50, 101);

            var activity = new FellWoodActivity (person, needEntry, settings, new ConsoleHelper(settings));

            var identifier = new AvailableTreeIdentifier (activity);

            Console.WriteLine ("");
            Console.WriteLine ("Executing target");
            Console.WriteLine ("");

            treesFound = identifier.IdentifyTreesToFell();

            Console.WriteLine ("");
            Console.WriteLine ("Checking results");
            Console.WriteLine ("");

            Assert.AreEqual (2, treesFound.Length);
        }

    }
}

