using System;
using tilesim.Entities;
using datamanager.Data;

namespace tilesim.Data
{
	public class PeopleKeys
	{
		public string GetPersonKey(Guid personId)
		{
			return DataConfig.Prefix + "-Person-" + personId.ToString ();
		}

		public string GetPeopleKey(Guid tileId)
		{
			return DataConfig.Prefix + "-Tile-" + tileId.ToString () + "-People";
		}
	}
}

