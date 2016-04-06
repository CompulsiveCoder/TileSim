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

		public int GameSpeed = 10;
		public int CycleDuration = 1000;
		#endregion

        #region Environment settings
        public int DefaultTileQuantity = 1;
        public int DefaultPeoplePerTile = 5;
        public int DefaultTreesPerTile = 100;
        #endregion

		#region Player settings
		public string PlayerId = "";
		#endregion

		#region Town settings
		// TODO: Remove if not needed
		//public int DefaultTownPopulation = 5;
		//public int DefaultTownTreeCount = 100;
		#endregion

        #region Person Settings
        public decimal ThirstThreshold = 50;
        public decimal DefaultDrinkAmount = 10;
        public decimal DefaultCollectWaterRate = 10;
        public decimal WaterForThirstRatio = 1;
        public decimal DefaultEatAmount = 10;
        public decimal FoodForHungerRatio = 1;
        public decimal HungerThreshold = 40;
        public decimal DefaultGatherFoodRate = 10;
        #endregion

		#region Wood Settings
		public decimal FellingRate = 10m;

		public decimal TreeGrowthRate = 0.0000001m;
		#endregion

		#region Timber Settings
		public decimal WoodRequiredForTimber = 1.8m;
		public decimal MinimumTreeSize = 10;
		#endregion

		#region Building Settings
		public decimal TimberNeededForHouse = 50;

		public double ConstructionRate = 0.2;

        public decimal ShelterTimberCost = 50;
		#endregion

		#region Milling Settings
		public decimal TimberMillingRate = 10;
		#endregion

        public Dictionary<ItemType, int> DefaultPriorities = new Dictionary<ItemType, int> ();

		public double AgingRate = 0.1;
		public int BirthOdds = 25; // 1 in 25


		public decimal HungerRate = 0.1m;//100m / (24*60*60) * 3m; // 100% / seconds in a day * meals per day


		//public int ConstructionCountLimitBase = 3;
		//public double ConstructionCountLimit = 0.1;

		#region Console settings
		public ConsoleOutputType OutputType = ConsoleOutputType.Debug;

		public bool IsVerbose = false;
		#endregion

		public EngineSettings()
        {
            DefaultPriorities.Add (ItemType.Drink, 100);
            DefaultPriorities.Add (ItemType.Water, 100);
            DefaultPriorities.Add (ItemType.Shelter, 90);
            DefaultPriorities.Add (ItemType.Meal, 80);
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
				settings.IsVerbose = true;
				return settings;
			}
		}
	}
}

