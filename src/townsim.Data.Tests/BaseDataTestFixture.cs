using System;
using NUnit.Framework;
using datamanager.Data;

namespace townsim.Data.Tests
{
	public class BaseDataTestFixture
	{
		private DataManager data;

		public BaseDataTestFixture ()
		{
		}

		[SetUp]
		public void Initialize()
		{
		}

		[TearDown]
		public void Finish()
		{
			if (data != null)
				data.Client.FlushAll ();
		}

		public DataManager GetDataManager()
		{
			if (data == null) {
				data = new DataManager ();
				data.Settings.Prefix = "Test-" + Guid.NewGuid ().ToString ();
			}

			return data;
		}
	}
}

