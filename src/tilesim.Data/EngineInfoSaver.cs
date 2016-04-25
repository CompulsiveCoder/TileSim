using System;
using tilesim.Entities;
using Sider;

namespace tilesim.Data
{
	public class EngineInfoSaver
	{
		public EngineInfoSaver ()
		{
		}

		public void Save(EngineInfo info)
		{
			var client = new RedisClient();
			var key = new EngineKeys ().GetInfoKey (info.Id);
			var json = info.ToJson ();
			client.Set(key, json);
		}
	}
}

