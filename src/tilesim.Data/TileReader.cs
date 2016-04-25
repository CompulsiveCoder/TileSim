using System;
using Sider;
using tilesim.Entities;

namespace tilesim.Data
{
	public class TileReader
	{
		public TileReader ()
		{
		}

		public Tile Read(string tileId)
		{
			var client = new RedisClient();
			var json = client.Get (new TileKeys ().GetTileKey (tileId));

			var tile = new Parser().Parse<Tile> (json);

			// Buildings
			var buildingReader = new BuildingReader ();
			tile.Buildings = new BuildingCollection (buildingReader.Read (tile.Id));

			foreach (var person in tile.People)
				person.Location = tile;

			return tile;
		}
	}
}

