using System;
using NUnit.Framework;
using townsim.Data.Tests;
using townsim.Engine.Activities;
using townsim.Entities;
using townsim.Engine.Needs;

namespace townsim.Engine.Tests.Unit.Activities
{
	[TestFixture]
	public class MillTimberActivityTestFixture : BaseEngineUnitTestFixture
	{
		[Test]
		public void Test_Act_WoodIsAvailable()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

			var context = MockEngineContext.New ();
			context.Settings.IsVerbose = true;
			context.Settings.TimberMillingRate = context.Settings.TimberMillingRate * 10;

			var person = new PeopleCreator ().CreateAdult ();
			person.Supplies[NeedType.Wood] = 100;

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);

			var needEntry = new NeedEntry (NeedType.Timber, 50, 101);

			var activity = new MillTimberActivity (person, needEntry, context.Settings);

			Console.WriteLine ("");
			Console.WriteLine ("Executing test");
			Console.WriteLine ("");

			activity.Act (person);

			Console.WriteLine ("");
			Console.WriteLine ("Analysing test");
			Console.WriteLine ("");

			Assert.AreEqual (50, person.Supplies [NeedType.Timber]);
			Assert.AreEqual (10, person.Supplies [NeedType.Wood]);
		}

		[Test]
		public void Test_Mill_NotEnoughWood()
		{
			Console.WriteLine ("");
			Console.WriteLine ("Preparing test");
			Console.WriteLine ("");

			var context = MockEngineContext.New ();
			context.Settings.IsVerbose = true;

			var person = new PeopleCreator ().CreateAdult ();

			var tile = context.World.Tiles[0];
			tile.AddPerson (person);

			var needEntry = new NeedEntry (NeedType.Timber, 50, 101);

			var activity = new MillTimberActivity (person, needEntry, context.Settings);

			Console.WriteLine ("");
			Console.WriteLine ("Executing test");
			Console.WriteLine ("");

			activity.Act (person);

			var foundNeedEntry = person.Needs [0];

			Assert.AreEqual (NeedType.Wood, foundNeedEntry.Type);
			Assert.AreEqual (90, foundNeedEntry.Quantity);
			Assert.AreEqual (102, foundNeedEntry.Priority);
		}
	}
}

