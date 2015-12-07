using System;
using Newtonsoft.Json;
using datamanager.Entities;

namespace townsim.Entities
{
	[Serializable]
	[JsonObject("Building", IsReference=true)]
	public class Building : BaseEntity, IActivityTarget
	{
		public bool IsCompleted { get; set; }
		public double PercentComplete { get; set; }
		public BuildingType Type { get; set; }
		public int TimberPending
		{
			get {
				return TimberCost - Timber;
			}
		}
		public int Timber { get; set; }
		public int TimberCost { get; set; }

		[TwoWay("Home")]
		public Person[] People { get;set; }

		public TimeSpan ConstructionStartTime { get; set; }

		public TimeSpan ConstructionEndTime { get; set; }

		public TimeSpan ConstructionDuration
		{
			get {
				if (ConstructionEndTime > ConstructionStartTime)
					return ConstructionEndTime.Subtract (ConstructionStartTime);
				else
					return new TimeSpan (0);//return DateTime.Now.Subtract (ConstructionStartTime);
			}
			set { }
		}

		public Building ()
		{
			Construct ();

			TimberCost = 50;
		}

		public Building (BuildingType type)
		{
			Construct ();

			TimberCost = 50;
			Type = type;
		}

		public void Construct()
		{
			People = new Person[]{ };
		}
	}
}

