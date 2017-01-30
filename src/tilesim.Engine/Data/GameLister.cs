using System;
using datamanager.Data;
using tilesim.Engine.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using datamanager.Data.Providers.Redis;

namespace tilesim.Data
{
    public class GameLister
    {
        public GameLister ()
        {
        }

        public EngineInfo[] ListGames()
        {
            var provider = new RedisDataProvider ();

            var data = new DataManager (provider);

            data.Settings.IsVerbose = true;

            Console.WriteLine ("Listing games");

            var dataBypasser = new DataManagerPrefixBypasser (data.Settings, data.Provider);

            var keys = dataBypasser.FindKeysFromType (typeof(EngineInfo));

            var list = new List<EngineInfo> ();

            foreach (var key in keys)
            {
                var content = data.Provider.Get (key);
                var engineInfo = (EngineInfo)JsonConvert.DeserializeObject (content, typeof(EngineInfo));
                list.Add(engineInfo);
            }

            return list.ToArray();
        }
    }
}

