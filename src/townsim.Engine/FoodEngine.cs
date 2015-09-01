using System;
using townsim.Entities;
using townsim.Alerts;

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
			var numberOfPeople = town.Population * EatRate;

			town.FoodSources -= numberOfPeople;
			if (town.FoodSources < 0)
				town.FoodSources = 0;

			if (numberOfPeople > town.FoodSources) {
				UpdateStarvation (town);
			}
		}

		public void UpdateStarvation(Town town)
		{
			var foodRequired = town.Population * EatRate;
			if (foodRequired > town.FoodSources) {
				var shortage = foodRequired - town.FoodSources;
				//var numberOfPeople = (int)(town.Population * 0.05);
				var numberOfPeople = new Random().Next(0, (int)shortage);
				var populationEngine = new PopulationEngine ();
				if (numberOfPeople > 0) {
					town.AddAlert (new StarvationAlert ());
					populationEngine.Die (town, numberOfPeople);
				} else
					town.AddAlert (new HungerAlert ());
			}
		}
	}
}

