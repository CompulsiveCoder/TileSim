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
		public int TimberPending
		{
			get {
				return TimberCost - TimberAvailable;
			}
		}
		public int TimberAvailable { get; set; }
		public int TimberCost { get; set; }

		public Building ()
		{
			TimberCost = 50;
		}

		public Building (BuildingType type)
		{
			Type = type;
			TimberCost = 50;
		}
	}
}

