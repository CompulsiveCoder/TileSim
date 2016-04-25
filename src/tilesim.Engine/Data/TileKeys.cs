using System;
using datamanager.Data;

namespace tilesim.Data
{
	public class TileKeys
	{
		public string GetTileKey(string tileId)
		{
			return DataConfig.Prefix + "-Tile-" + tileId;
		}
	}
}

