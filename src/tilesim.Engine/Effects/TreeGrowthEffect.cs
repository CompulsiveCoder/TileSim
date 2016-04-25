using System;
using tilesim.Data;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Effects
{
	public class TreeGrowthEffect : BaseEffect
	{
		// TODO: Merge with plant growth effect
		public EngineInfo Info { get;set; }

		public TreeGrowthEffect (EngineContext context) : base(context)
		{
		}

		public void Update(Tile tile)
		{
			UpdateGrowth (tile);
		}

		public void UpdateGrowth(Tile tile)
		{
			foreach (var tree in tile.Trees) {
				if (tree.PercentPlanted >= 100) {
					var growthAmount = tree.Size * Context.Settings.TreeGrowthRate;
					tree.Size += growthAmount;
				}
			}
		}
	}
}

