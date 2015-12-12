using System;
using townsim.Entities;

namespace townsim.Engine.Effects
{
	public class PlantGrowthEffect
	{
		public PlantGrowthEffect ()
		{
		}

		public void Update(Plant plant)
		{
			if (plant.PercentPlanted >= 100) {
				plant.Age += 0.1m;
				plant.Size += 0.3m;
			}
		}
	}
}

