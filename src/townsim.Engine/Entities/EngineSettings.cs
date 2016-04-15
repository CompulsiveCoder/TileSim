using System;
using townsim.Engine;
using System.Collections.Generic;

namespace townsim.Engine.Entities
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
        public int DefaultTileQuantity = 1;
        public int DefaultWaterPerTile = 50000;
        public int DefaultPeoplePerTile = 5;
        public int DefaultTreesPerTile = 100;
        public int DefaultFoodPerTile = 1000;
        #endregion

		#region Player settings
		public string PlayerId = "";
		#endregion

        #region Person Settings
        public decimal StarvationThreshold = 90;
        public decimal DehydrationThreshold = 80;
        public decimal ThirstThreshold = 50;
        public decimal DefaultDrinkAmount = 5;
        public decimal DefaultCollectWaterRate = 10;
        public decimal WaterForThirstRatio = 1;
        public decimal DefaultEatAmount = 10;
        public decimal FoodForHungerRatio = 1;
        public decimal HungerThreshold = 40;
        public decimal DefaultGatherFoodRate = 10;

        public decimal ThirstRate = 0.03m;
        public decimal HungerRate = 0.02m;
        #endregion

		#region Wood Settings
		public decimal FellingRate = 0.1m;

		public decimal TreeGrowthRate = 0.0000001m;
		#endregion

		#region Timber Settings
		public decimal WoodRequiredForTimber = 1.8m;
		public decimal MinimumTreeSize = 10;
		#endregion

		#region Building Settings
		public decimal TimberNeededForHouse = 50; // TODO: Remove if not needed

		public double ConstructionRate = 0.01;

        public decimal ShelterTimberCost = 50;
		#endregion

		#region Milling Settings
		public decimal TimberMillingRate = 0.01m;
		#endregion

        public Dictionary<ItemType, int> DefaultPriorities = new Dictionary<ItemType, int> ();

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
            DefaultPriorities.Add (ItemType.Water, 100);
            DefaultPriorities.Add (ItemType.Shelter, 90);
            DefaultPriorities.Add (ItemType.Food, 80);
            DefaultPriorities.Add (ItemType.Timber, 0);
            DefaultPriorities.Add (ItemType.Wood, 0);
		}

		public EngineSettings(int gameSpeed)
		{
			GameSpeed = gameSpeed;
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

