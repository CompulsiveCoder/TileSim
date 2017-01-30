using System;
using NUnit.Framework;
using tilesim.Engine.Entities;
using datamanager.Data;

namespace tilesim.Data.Tests.Integration
{
    // TODO: Remove or reimplement
    //[TestFixture(Category="Integration")]
	public class DataManagerIntegrationTest : BaseDataTestFixture
	{
		//[Test]
		public void Test_GeneralFunctions()
		{
			var data = GetDataManager();

            var settings = EngineSettings.DefaultVerbose;

			var person = new Person(settings);

			data.Save (person);

			var loadedTile = data.Get<Person>(person.Id);

			Assert.IsNotNull (loadedTile);

			var loadedPeople = data.Get<Person>();

			Assert.IsNotNull (loadedPeople);
			Assert.AreEqual (1, loadedPeople.Length);

			data.Delete (person);

			loadedTile = data.Get<Person>(person.Id);

			Assert.IsNull (loadedTile);
		}
	}
}

