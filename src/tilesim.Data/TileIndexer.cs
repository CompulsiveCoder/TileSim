using System;
using Sider;
using System.Collections.Generic;
using tilesim.Entities;

namespace tilesim.Data
{
	public class TileIndexer
	{
		public TileIndexer ()
		{
		}

		public Tile[] Get()
		{
			var idManager = new DataIdManager ();
			var ids = idManager.GetIds(typeof(Tile));

			var tiles = new List<Tile> ();
			var reader = new TileReader ();
			foreach (Guid id in ids) {
				tiles.Add (reader.Read (id));
			}
			return tiles.ToArray();
		}
	}
}

