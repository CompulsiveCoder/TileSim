using System;
using Sider;
using townsim.Entities;

namespace townsim.Data
{
	public class BuildingSaver : BaseDataAdapter
	{
		public BuildingSaver ()
		{
		}

		public void Save(Town town, Building[] buildings)
		{
			var client = new RedisClient();
			var json = ArrayToJson (buildings);
			var key = new BuildingKeys ().GetBuildingsKey (town.Id);
			client.Set(key, json);

			//var idManager = new DataIdManager ();
			//idManager.Add (town);
		}
	}
}

