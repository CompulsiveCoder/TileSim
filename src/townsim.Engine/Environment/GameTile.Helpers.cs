using System;
using Newtonsoft.Json;
using townsim.Engine.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public partial class GameTile
	{
		[JsonIgnoreAttribute]
		public Plant[] Trees
		{
			get{
				// TODO: Use linq
				var list = new List<Plant> ();

				foreach (var plant in Plants)
					if (plant.Type == PlantType.Tree)
						list.Add (plant);

				return list.ToArray ();
			}
		}

		public GameTile ()
		{
		}
	}
}

