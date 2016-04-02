using System;
using Newtonsoft.Json;
using datamanager.Entities;

namespace townsim.Engine.Entities
{
	[Serializable]
	[JsonObject("Building", IsReference=true)]
    public class Building : BaseGameEntity, IActivityTarget, IHasInventory
	{
        public bool IsCompleted { get { return PercentComplete == 100; } }

		public double PercentComplete { get; set; }
		public BuildingType Type { get; set; }
		public decimal TimberPending
		{
			get {
                return Settings.ShelterTimberCost - Inventory.Items[ItemType.Timber];
			}
		}

		[TwoWay("Home")]
		public Person[] People { get;set; }

        public Inventory Inventory { get;set; }

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

		[TwoWay("Buildings")]
		public Town Town { get; set; }

        public EngineSettings Settings { get; set; }

        public Building (EngineSettings settings)
		{
			Construct (settings);

		}

        public Building (BuildingType type, EngineSettings settings)
		{
			Construct (settings);

			Type = type;
		}

        public void Construct(EngineSettings settings)
		{
			People = new Person[]{ };
            Inventory = new Inventory (this, settings);
            Settings = settings;
		}
	}
}

