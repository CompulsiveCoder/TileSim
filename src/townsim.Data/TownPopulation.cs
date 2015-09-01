using System;
using Sider;

namespace townsim.Data
{
	public class TownPopulation
	{
		public TownPopulation ()
		{
		}

		public int ReadPopulationCount(Guid townId)
		{
			var client = new RedisClient();
			var key = new PeopleKeys ().GetPopulationKey (townId);
			var value = client.Get (key);
			return Convert.ToInt32(value);
		}

		public void SetPopulationCount(Guid townId, int population)
		{
			var client = new RedisClient();
			var key = new PeopleKeys ().GetPopulationKey (townId);
			client.Set(key, population.ToString());
		}
	}
}

