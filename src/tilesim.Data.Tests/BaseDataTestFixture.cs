using System;
using NUnit.Framework;
using datamanager.Data;
using datamanager.Data.Tests;
using tilesim.Tests;

namespace tilesim.Data.Tests
{
    public class BaseDataTestFixture : BaseTestFixture
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
                data = new MockDataManager ();
                data.Settings.Prefix = "Test-" + Guid.NewGuid ().ToString ();
            }

            return data;
        }
	}
}

