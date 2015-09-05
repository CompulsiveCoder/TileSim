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
			plant.Age += 0.1;
			plant.Size += 0.3;
		}
	}
}

