using System;
using Sider;
using tilesim.Entities;

namespace tilesim.Data
{
	public class TileDeleter 
	{
		public TileDeleter ()
		{
		}

		public void Delete(Tile tile)
		{
			var client = new RedisClient();
			client.Del(new TileKeys().GetTileKey(tile.Id));

			new DataIdManager ().Remove (tile);
		}
	}
}

