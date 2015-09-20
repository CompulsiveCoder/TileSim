using System;
using townsim.Entities;
using Sider;

namespace townsim.Data
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

