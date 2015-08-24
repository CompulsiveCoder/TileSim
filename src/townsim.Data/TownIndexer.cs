using System;
using Sider;
using System.Collections.Generic;

namespace townsim.Data
{
	public class TownIndexer
	{
		public TownIndexer ()
		{
		}

		public Town[] Get()
		{
			var idManager = new DataIdManager ();
			var ids = idManager.GetIds(typeof(Town));

			var towns = new List<Town> ();
			var reader = new TownReader ();
			foreach (Guid id in ids) {
				towns.Add (reader.Read (id));
			}
			return towns.ToArray();
		}
	}
}

