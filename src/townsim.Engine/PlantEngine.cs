using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class PlantEngine
	{
		public PlantEngine ()
		{
		}

		public void Update(Plant plant)
		{
			if (plant.PercentPlanted == 100) {
				plant.Age += 0.1;
				plant.Size += 0.3;
			}
		}
	}
}

