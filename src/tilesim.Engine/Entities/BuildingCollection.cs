using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;

namespace tilesim.Engine.Entities
{
	[Serializable]
	[JsonObject("Buildings")]
	public class BuildingCollection : List<Building>, IEnumerable
	{
		public BuildingCollection ()
		{
		}

		public BuildingCollection(Building[] buildings)
		{
			AddRange(buildings);
		}

	}
}
