using System;
using NUnit.Framework;
using townsim.Engine.Entities;
using datamanager.Data;

namespace townsim.Data.Tests.Integration
{
	[TestFixture]
	public class DataManagerIntegrationTest : BaseDataTestFixture
	{
		[Test]
		public void Test_GeneralFunctions()
		{
			var data = GetDataManager();

			var person = new Person();

			data.Save (person);

			var loadedTown = data.Get<Person>(person.Id);

			Assert.IsNotNull (loadedTown);

			var loadedPeople = data.Get<Person>();

			Assert.IsNotNull (loadedPeople);
			Assert.AreEqual (1, loadedPeople.Length);

			data.Delete (person);

			loadedTown = data.Get<Person>(person.Id);

			Assert.IsNull (loadedTown);
		}
	}
}

