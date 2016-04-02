using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Effects
{
	public class PlantGrowthEffect : BaseEffect
	{
		public PlantGrowthEffect (EngineContext context) : base(context)
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

