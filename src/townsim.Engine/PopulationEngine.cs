using System;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine
{
	public class PopulationEngine
	{
		public PopulationEngine ()
		{
		}

		public void Update(Town town)
		{
			UpdatePopulationBirthRate (town);
			UpdatePopulationDeaths (town);
			UpdatePopulationMigration (town);
		}

		public void UpdatePopulationBirthRate(Town town)
		{
			if (town.Population > 100)
				town.Population += town.Population / 50;
			else
				town.Population += town.Population / 20;
		}

		public void UpdatePopulationDeaths(Town town)
		{
			// General deaths
			town.Population = town.Population - (town.Population / 50);

			// Thirst
			if (town.Population > town.WaterSources) {
				var quantity = town.Population / 20;
				town.Population -= quantity;
				//town.Population = town.Population - (town.Population / 50);
			}
		}

		public void UpdatePopulationMigration(Town town)
		{
			// Arriving
			var probability = new Random ().Next (100);
			if (probability > 90)
			{
				var value = new Random ().Next (3);
				town.Population += value;
			}

			// Leaving
			var leavingProbability = new Random ().Next (100);
			if (leavingProbability < town.TotalHomelessPeople) {
				var value = new Random ().Next (3);
				town.Population += value;
			}
		}
	}
}

