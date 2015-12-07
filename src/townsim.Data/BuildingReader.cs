using System;
using Sider;
using townsim.Entities;

namespace townsim.Data
{
	public class BuildingReader : BaseDataAdapter
	{
		public BuildingReader ()
		{
		}

		public Building[] Read(string townId)
		{
			return new datamanager.Data.DataManager ().Get (townId);
			/*
			var client = new RedisClient();
			var key = new BuildingKeys ().GetBuildingsKey (townId);
			var json = client.Get (key);

			var buildings = JsonToArray<Building> (json);

			return buildings;*/
		}
	}
}

