using System;
using townsim.Data;

namespace townsim.EngineConsole
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
			town.Population = town.Population - (town.Population / 50);
		}

		public void UpdatePopulationMigration(Town town)
		{
			var probability = new Random ().Next (100);
			if (probability > 90)
			{
				var value = new Random ().Next (5);
				town.Population += value;
			}
		}
	}
}

