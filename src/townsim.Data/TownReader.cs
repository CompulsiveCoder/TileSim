using System;
using Sider;

namespace townsim.Data
{
	public class TownReader
	{
		public TownReader ()
		{
		}

		public Town Read(Guid townId)
		{
			var client = new RedisClient();
			var json = client.Get (new TownKeys ().GetTownKey (townId));

			var town = new Parser().Parse<Town> (json);

			return town;
		}
	}
}

