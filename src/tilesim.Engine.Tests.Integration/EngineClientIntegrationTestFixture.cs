using System;
using NUnit.Framework;
using datamanager.Data;
using tilesim.Engine.Entities;
using datamanager.Data.Providers.Memory;
using Newtonsoft.Json;

namespace tilesim.Engine.Tests.Integration
{
    [TestFixture]
    public class EngineClientIntegrationTestFixture : BaseEngineIntegrationTestFixture
    {
        [Test]
        public void Test_New()
        {
            Console.WriteLine ("====================");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("====================");

            var data = GetDataManager ();

            // TODO: Should the base test fixture provide a helper function to create the EngineClient? Allowing its creation to be abstracted away from
            // individual unit tests to provide common functionality
            var client = new EngineClient (data);
            client.IsVerbose = true;

            Console.WriteLine ("");
            Console.WriteLine ("====================");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("====================");

            var context = client.New ();

            Console.WriteLine ("");
            Console.WriteLine ("====================");
            Console.WriteLine ("Reviewing test");
            Console.WriteLine ("====================");

            var enginesFound = data.Get<EngineInfo> ();

            Assert.AreEqual (1, enginesFound.Length);

            Assert.AreEqual (context.Settings.EngineId, enginesFound [0].Id);
        }

        [Test]
        public void Test_List()
        {
            Console.WriteLine ("====================");
            Console.WriteLine ("Preparing test");
            Console.WriteLine ("====================");

            var data = GetDataManager ();
            var internalData = ((MemoryDataProvider)data.Provider).Data;

            // TODO: Should the base test fixture provide a helper function to create the EngineClient? Allowing its creation to be abstracted away from
            // individual unit tests to provide common functionality
            var client = new EngineClient (data);
            client.IsVerbose = true;

            var info = new EngineInfo (DateTime.Now, EngineSettings.Default);

            data.Save (info);

            // TODO: Remove if not needed. This is taking isolation unncessarily far for an integration test.
            /*var entityKey = data.Keys.GetKey (info);

            internalData [entityKey] = JsonConvert.SerializeObject (info);

            var idsKey = data.Keys.GetIdsKey (info.GetType());

            internalData [idsKey] = info.Id;

            var typesKey = data.Keys.GetTypesKey ();

            internalData [typesKey] = info.TypeName;*/

            Console.WriteLine ("");
            Console.WriteLine ("====================");
            Console.WriteLine ("Executing test");
            Console.WriteLine ("====================");

           // var context = client.New ();

            Console.WriteLine ("");
            Console.WriteLine ("====================");
            Console.WriteLine ("Reviewing test");
            Console.WriteLine ("====================");

            var enginesFound = client.ListEngines ();

            Assert.AreEqual (1, enginesFound.Length);

            //Assert.AreEqual (context.Settings.EngineId, enginesFound [0].Id);
        }
    }
}

