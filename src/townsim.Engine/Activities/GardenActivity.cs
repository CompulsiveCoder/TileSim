using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	public class GardenActivity
	{
		public WorkersUtility Workers = new WorkersUtility();

		public double PlantingTimeCost = 70;

		public EngineSettings Settings;

		public EngineClock Clock;

		public GardenActivity (EngineSettings settings, EngineClock clock)
		{
			Settings = settings;
			Clock = clock;
		}

		public void Update(Town town)
		{
			//var treesPlantedToday = town.CountVegetablesPlantedToday (Clock.GameDuration);

			//if (treesPlantedToday < town.VegetablesToPlantPerDay)
			//	HireWorkers (town);

			DoPlanting (town);
		}

		public void HireWorkers(Town town)
		{
			if (town.TotalInactive > 0) {
				var treesToPlant = town.VegetablesToPlantPerDay;

				var workersNeeded = treesToPlant;

				for (int i = 0; i < workersNeeded; i++) {
					var plant = new Plant (PlantType.Vegetable);
					plant.TimePlanted = Clock.GameDuration;
					plant.WasPlanted = true;

					Workers.Hire (town, 1, ActivityType.Gardening, plant);

					if (plant.People.Length > 0) {
						var plants = new List<Plant> (town.Plants);
						plants.Add (plant);
						town.Plants = plants.ToArray ();
					}
				}
			}
		}

		public void DoPlanting(Town town)
		{
			foreach (var person in town.People) {
				if (person.Activity == ActivityType.Gardening) {
					var plant = (Plant)person.ActivityTarget;

					if (plant == null) {
						plant = new Plant (PlantType.Vegetable);
						plant.TimePlanted = Clock.GameDuration;
						plant.WasPlanted = true;

						var plants = new List<Plant> (town.Plants);
						plants.Add (plant);
						town.Plants = plants.ToArray ();
					}

					DoPlanting (plant);

					if (plant.PercentPlanted >= 100) {
						town.TotalVegetablesPlanted++;
						person.Finish ();

						LogWriter.Current.AppendLine (CurrentEngine.Id, "A vegetable seedling has been planted.");
					} else {
						DoPlanting (plant);
					}
				}
			}
		}

		public void DoPlanting(Plant plant)
		{
			if (plant.PercentPlanted < 100) {
				var increment = GetPlantingCompletionIncrement ();
				plant.PercentPlanted += increment;
			}

			if (plant.PercentPlanted > 100) {
				plant.PercentPlanted = 100;
				plant.WasPlanted = true;
			}
		}

		public double GetPlantingCompletionIncrement()
		{
			var increment = 100 / PlantingTimeCost;

			return increment;
		}
	}
}

