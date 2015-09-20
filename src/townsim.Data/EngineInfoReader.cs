using System;
using townsim.Entities;
using Sider;

namespace townsim.Data
{
	public class EngineInfoReader : BaseDataAdapter
	{
		public EngineInfoReader ()
		{
			
		}
	
		public EngineInfo Read(Guid engineId)
		{
			var client = new RedisClient ();
			var key = new EngineKeys ().GetInfoKey (engineId);

			if (!client.Exists (key))
				return null;
			else {
				var json = client.Get (key);

				var engineInfo = JsonToEntity<EngineInfo> (json);

				return engineInfo;
			}
		}
	}
}

