﻿using System;
using townsim.Engine;

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

		#region Player settings
		public string PlayerId = "";
		#endregion

		#region Town settings
		// TODO: Remove if not needed
		//public int DefaultTownPopulation = 5;
		//public int DefaultTownTreeCount = 100;
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
		#endregion

		#region Milling Settings
		public decimal TimberMillingRate = 10;
		#endregion


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

