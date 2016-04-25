using System;
using Sider;
using tilesim.Entities;

namespace tilesim.Data
{
	public class BuildingSaver : BaseDataAdapter
	{
		public BuildingSaver ()
		{
		}

		public void Save(Tile tile, Building[] buildings)
		{
			var client = new RedisClient();
			var json = ArrayToJson (buildings);
			var key = new BuildingKeys ().GetBuildingsKey (tile.Id);
			client.Set(key, json);

			//var idManager = new DataIdManager ();
			//idManager.Add (tile);
		}
	}
}

