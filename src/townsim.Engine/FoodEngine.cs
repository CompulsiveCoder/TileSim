using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class FoodEngine
	{
		public double EatRate = 1;

		public FoodEngine ()
		{
		}

		public void Update(Town town)
		{
			UpdateEating (town);
		}

		public void UpdateEating(Town town)
		{
			var amount = town.Population * EatRate;

			town.FoodSources -= amount;
			if (town.FoodSources < 0)
				town.FoodSources = 0;

			if (amount > town.FoodSources) {
				UpdateStarvation (town);
			}
		}

		public void UpdateStarvation(Town town)
		{
			var amount = town.Population * 0.05;
			town.Population = town.Population - (int)amount;

		}
	}
}

