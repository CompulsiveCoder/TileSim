using System;
using Sider;

namespace townsim.Data
{
	public class TownDeleter 
	{
		public TownDeleter ()
		{
		}

		public void Delete(Town town)
		{
			var client = new RedisClient();
			client.Del(new TownKeys().GetTownKey(town.Id));

			new DataIdManager ().Remove (town);
		}
	}
}

