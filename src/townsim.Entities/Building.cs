using System;
using Newtonsoft.Json;

namespace townsim.Entities
{
	[Serializable]
	[JsonObject("Building")]
	public class Building : BaseEntity, IEmploymentTarget
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

		public Person[] Workers { get;set; }

		public Building ()
		{
			TimberCost = 50;
		}

		public Building (BuildingType type)
		{
			TimberCost = 50;
			Type = type;
		}
	}
}

