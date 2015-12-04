using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class ForestsEngine
	{
		public EngineSettings Settings { get;set; }

		public double TreeGrowthRate = 0.0000001;

		public ForestsEngine (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Town town)
		{
			UpdateGrowth (town);
		}

		public void UpdateGrowth(Town town)
		{
			foreach (var tree in town.Trees) {
				if (tree.PercentPlanted >= 100) {
					var growthAmount = tree.Size * TreeGrowthRate * Settings.GameSpeed;
					tree.Size += growthAmount;
				}
			}
		}
	}
}

