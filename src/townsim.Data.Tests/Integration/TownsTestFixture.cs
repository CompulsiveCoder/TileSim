using System;
using NUnit.Framework;

namespace townsim.Data.Tests.Integration
{
	[TestFixture]
	public class TownsTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SaveAndGet()
		{
			var saver = new TownSaver ();
			var town = new Town ("TestTown", 10);
			saver.Save (town);

			var reader = new TownReader ();
			var loadedTown = reader.Read (town.Id);

			Assert.IsNotNull (loadedTown);

			var indexer = new TownIndexer ();
			var loadedTowns = indexer.Get();

			Assert.IsNotNull (loadedTowns);
			Assert.AreEqual (1, loadedTowns.Length);

			var deleter = new TownDeleter ();
			deleter.Delete (town);
		}
	}
}

