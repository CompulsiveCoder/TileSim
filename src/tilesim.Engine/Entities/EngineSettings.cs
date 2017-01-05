using System;
using tilesim.Engine;
using System.Collections.Generic;

namespace tilesim.Engine.Entities
{
	[Serializable]
	public class EngineSettings
	{
		#region Engine settings
		public string EngineId = "";

		public int GameSpeed = 1;
		public int CycleDuration = 1000;
		#endregion

        #region Environment settings
        public int HorizontalTileCount = 5;
        public int VerticalTileCount = 5;
        public int DefaultWaterPerTile = 50000;
        public int DefaultPeoplePerTile = 5;
        public int DefaultTreesPerTile = 100;
        public int DefaultFoodPerTile = 10000;
        #endregion

		#region Player settings
		public string PlayerId = "";

        public PersonSettings PlayerSettings { get; set; }
		#endregion

        #region Person Settings
        public decimal StarvationThreshold = 90;
        public decimal DehydrationThreshold = 80;
        public decimal ThirstThreshold = 50;
        public decimal DefaultDrinkAmount = 5;
        public decimal DefaultCollectWaterRate = 10;
        public decimal WaterForThirstRatio = 1;
        public decimal DefaultEatAmount = 10;
        public decimal FoodForHungerRatio = 0.5m;
        public decimal HungerThreshold = 40;
        public decimal DefaultGatherFoodRate = 10;
        public decimal PersonEnergyConsumptionRate = 0.001m;

        public decimal ThirstRate = 0.003m;
        public decimal HungerRate = 0.002m;

        public PersonSettings PersonSettings { get; set; }
        #endregion

		#region Wood Settings
		public decimal TimberFellingRate = 1m;

		public decimal TreeGrowthRate = 0.0000001m;

        public decimal TreeHeightToWoodAmountRatio = 1;
		#endregion

		#region Timber Settings
		public decimal WoodRequiredForTimber = 1.8m;
		public decimal MinimumTreeSize = 10;
		#endregion

		#region Building Settings
		public decimal TimberNeededForHouse = 50; // TODO: Remove if not needed

		public decimal ConstructionRate = 0.005m;

        public decimal ShelterTimberCost = 50;
		#endregion

		#region Milling Settings
		public decimal TimberMillingRate = 0.2m;
		#endregion

        #region Sleep settings
        public decimal EnergyFromSleepRate = 0.2m;
        public decimal EnergySleepThreshold = 5;
        #endregion

        public Dictionary<ItemType, int> DefaultItemPriorities = new Dictionary<ItemType, int> ();
        public Dictionary<PersonVitalType, int> DefaultVitalPriorities = new Dictionary<PersonVitalType, int> ();

		#region Console settings
		public ConsoleOutputType OutputType = ConsoleOutputType.Debug;

        /// <summary>
        /// Set the OutputType property to change this.
        /// </summary>
        /// <value></value>
		public bool IsVerbose
        {
            get { return OutputType == ConsoleOutputType.Debug; }
        }
		#endregion

		public EngineSettings()
        {
            Construct ();
		}

		public EngineSettings(int gameSpeed)
		{
			GameSpeed = gameSpeed;

            Construct ();
		}

        public void Construct()
        {
            // TODO: Item priorities should be obsolete. Use vital priorities instead.
            DefaultItemPriorities.Add (ItemType.Water, 100);
            DefaultItemPriorities.Add (ItemType.Shelter, 80);
            DefaultItemPriorities.Add (ItemType.Food, 90);
            DefaultItemPriorities.Add (ItemType.Timber, 0);
            DefaultItemPriorities.Add (ItemType.Wood, 0);

            DefaultVitalPriorities.Add (PersonVitalType.Thirst, 100);
            DefaultVitalPriorities.Add (PersonVitalType.Energy, 95);
            DefaultVitalPriorities.Add (PersonVitalType.Hunger, 90);

            PlayerSettings = PersonSettings.SemiAutonomous;
            PersonSettings = PersonSettings.Autonomous;
        }

		public static EngineSettings Default
		{
			get { return new EngineSettings (); }
		}

		public static EngineSettings DefaultVerbose
		{
			get {
				var settings = new EngineSettings ();
                settings.OutputType = ConsoleOutputType.Debug;
				return settings;
			}
		}
	}
}

