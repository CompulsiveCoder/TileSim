using System;
using Newtonsoft.Json;
using datamanager.Entities;

namespace tilesim.Engine.Entities
{
	[Serializable]
	[JsonObject("Building", IsReference=true)]
    public class Building : BaseGameEntity, IHasInventory
	{
        public bool IsCompleted { get { return PercentComplete == 100; } }

        private decimal percentComplete;
		public decimal PercentComplete
        {
            get { return percentComplete; }
        }

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

        public void SetPercentComplete(decimal percentComplete)
        {
            var validatedPercentComplete = PercentageValidator.Validate (percentComplete);

            // TODO: See if this can be implemented. Console is not currently available here.
            //Console.WriteDebugLine ("    Setting \"Percent Complete\" to " + validatedPercentComplete);

            this.percentComplete = validatedPercentComplete;
        }

        public void IncreasePercentComplete(decimal percentageIncrease)
        {
            // TODO: See if this can be implemented. Console is not currently available here.
            //Console.WriteDebugLine ("    Increasing \"Percent Complete\" by " + percentageIncrease);

            percentComplete += percentageIncrease;

            percentComplete = PercentageValidator.Validate (percentComplete);
        }
	}
}

