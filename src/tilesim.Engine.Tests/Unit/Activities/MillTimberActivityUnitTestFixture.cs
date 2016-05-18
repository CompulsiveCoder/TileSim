using System;
using NUnit.Framework;
using tilesim.Data.Tests;
using tilesim.Engine.Activities;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Tests.Unit.Activities
{
    [TestFixture(Category="Unit")]
	public class MillTimberActivityUnitTestFixture : BaseEngineUnitTestFixture
	{
		[Test]
		public void Test_Act_WoodIsAvailable()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

            var context = MockEngineContext.New (EngineSettings.DefaultVerbose);
			context.Settings.TimberMillingRate = 50;

            var person = new Person(context.Settings);
            person.Inventory.Items[ItemType.Wood] = 100;

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);

            var needEntry = new NeedEntry (ActionType.Mill, ItemType.Timber, PersonVitalType.NotSet, 50, 101);

            var activity = new MillTimberActivity (person, needEntry, context.Settings, context.Console);

			Console.WriteLine ("");
			Console.WriteLine ("Executing test");
			Console.WriteLine ("");

			activity.Act (person);

			Console.WriteLine ("");
			Console.WriteLine ("Analysing test");
			Console.WriteLine ("");

            Assert.AreEqual (50, person.Inventory.Items [ItemType.Timber]);
            Assert.AreEqual (10, person.Inventory.Items [ItemType.Wood]);
		}

		[Test]
		public void Test_Mill_NotEnoughWood()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

            var context = MockEngineContext.New (EngineSettings.DefaultVerbose);

            var person = new Person (context.Settings);

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);

            var needEntry = new NeedEntry (ActionType.Mill, ItemType.Timber, PersonVitalType.NotSet, 50, 101);

            var activity = new MillTimberActivity (person, needEntry, context.Settings, context.Console);

			Console.WriteLine ("");
			Console.WriteLine ("Executing test");
			Console.WriteLine ("");

			activity.Act (person);

			var foundNeedEntry = person.Needs [0];

			Assert.AreEqual (ItemType.Wood, foundNeedEntry.ItemType);
			Assert.AreEqual (90, foundNeedEntry.Quantity);
			Assert.AreEqual (102, foundNeedEntry.Priority);
		}
	}
}

