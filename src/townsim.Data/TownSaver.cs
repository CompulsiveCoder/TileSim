using System;
using Sider;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace townsim.Data
{
	public class TownSaver
	{

		public TownSaver ()
		{
			
		}

		public void Save(Town town)
		{
			var client = new RedisClient();
			client.Set(new TownKeys().GetTownKey(town.Id), town.ToJson());

			var idManager = new DataIdManager ();
			idManager.Add (town);
		}
	}
}

