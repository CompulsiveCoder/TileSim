using System;
using Sider;
using townsim.Entities;

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

			var buildingReader = new BuildingReader ();
			town.Buildings = new BuildingCollection (buildingReader.Read (town.Id));


			return town;
		}
	}
}

