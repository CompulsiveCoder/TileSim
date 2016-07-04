using System;
using Sider;

namespace tilesim.Data
{
	public class TilePopulation
	{
		public TilePopulation ()
		{
		}

		public int ReadPopulationCount(Guid tileId)
		{
			var client = new RedisClient();
			var key = new PeopleKeys ().GetPopulationKey (tileId);
			var value = client.Get (key);
			return Convert.ToInt32(value);
		}

		public void SetPopulationCount(Guid tileId, int population)
		{
			var client = new RedisClient();
			var key = new PeopleKeys ().GetPopulationKey (tileId);
			client.Set(key, population.ToString());
		}
	}
}

