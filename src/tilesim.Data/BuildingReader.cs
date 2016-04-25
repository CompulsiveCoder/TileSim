using System;
using Sider;
using tilesim.Entities;

namespace tilesim.Data
{
	public class BuildingReader : BaseDataAdapter
	{
		public BuildingReader ()
		{
		}

		public Building[] Read(string tileId)
		{
			return new datamanager.Data.DataManager ().Get (tileId);
			/*
			var client = new RedisClient();
			var key = new BuildingKeys ().GetBuildingsKey (tileId);
			var json = client.Get (key);

			var buildings = JsonToArray<Building> (json);

			return buildings;*/
		}
	}
}

