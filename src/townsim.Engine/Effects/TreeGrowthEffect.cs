using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine.Effects
{
	public class TreeGrowthEffect : BaseEffect
	{
		// TODO: Merge with plant growth effect
		public EngineInfo Info { get;set; }

		public TreeGrowthEffect (EngineContext context) : base(context)
		{
		}

		public void Update(Town town)
		{
			UpdateGrowth (town);
		}

		public void UpdateGrowth(Town town)
		{
			foreach (var tree in town.Trees) {
				if (tree.PercentPlanted >= 100) {
					var growthAmount = tree.Size * Context.Settings.TreeGrowthRate;
					tree.Size += growthAmount;
				}
			}
		}
	}
}

