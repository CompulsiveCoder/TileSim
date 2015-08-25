using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class WaterSourcesEngine
	{
		public double ConsumptionRate = 1;

		public WaterSourcesEngine ()
		{
		}

		public void Update(Town town)
		{
			Rain (town);

			ConsumeWater (town);
		}

		public void ConsumeWater(Town town)
		{
			var amount = town.Population * ConsumptionRate;

			if (amount > town.WaterSources)
				town.Population -= 2;
			
			town.WaterSources -= amount;

			if (town.WaterSources < 0)
				town.WaterSources = 0;
		}

		public void Rain(Town town)
		{
			var probability = new Random ().Next (100);
			if (probability > 98)
			{
				var value = new Random ().Next (1000);
				town.WaterSources += value;
			}
		}
	}
}

