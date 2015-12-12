﻿using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine.Effects
{
	public class TreeGrowthEffect
	{
		// TODO: Merge with plant growth effect
		public EngineSettings Settings { get;set; }

		public decimal TreeGrowthRate = 0.0000001m;

		public TreeGrowthEffect (EngineSettings settings)
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
					var growthAmount = tree.Size * TreeGrowthRate;
					tree.Size += growthAmount;
				}
			}
		}
	}
}
