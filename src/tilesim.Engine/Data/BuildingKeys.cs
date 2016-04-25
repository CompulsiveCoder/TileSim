using System;
using datamanager.Data;

namespace tilesim.Data
{
	public class BuildingKeys
	{


		public string GetBuildingsKey(Guid tileId)
		{
			return DataConfig.Prefix + "-Tile-" + tileId.ToString () + "-Buildings";
		}
	}
}

