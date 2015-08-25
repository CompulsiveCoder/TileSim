using System;
using Newtonsoft.Json;

namespace townsim.Entities
{
	[Serializable]
	[JsonObject("Building")]
	public class Building : BaseEntity
	{
		public bool IsCompleted { get; set; }
		public double PercentComplete { get; set; }
		public int WorkerCount { get;set; }
		public BuildingType Type { get; set; }
		public int TimberNeeded { get; set; }
		public int TimberAvailable { get; set; }

		public Building ()
		{
			TimberNeeded = 50;
		}

		public Building (BuildingType type)
		{
			Type = type;
			TimberNeeded = 50;
		}
	}
}

