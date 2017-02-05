using System;
using datamanager.Data;
using datamanager.Data.Providers.Memory;

namespace tilesim.Tests
{
    public class BaseTestFixture
    {
        public BaseTestFixture ()
        {
        }

        public DataManager GetDataManager()
        {
            var data = new DataManager (new MemoryDataProvider());

            data.Settings.IsVerbose = true;

            return data;
        }
    }
}

