using System;
using townsim.Entities;
using System.Collections.Generic;

namespace townsim.Engine
{
	public class ForestryEngine
	{
		public WorkersEngine Workers = new WorkersEngine();

		public double PlantingTimeCost = 60; // Seconds

		public EngineSettings Settings;

		public EngineClock Clock;
		
		public ForestryEngine (EngineSettings settings, EngineClock clock)
		{
			Settings = settings;
			Clock = clock;
		}

		public void Update(Town town)
		{
			var treesPlantedToday = town.CountTreesPlantedToday (Clock.GameDuration);

			if (treesPlantedToday < town.TreesToPlantPerDay)
				HireWorkers (town);

			DoPlanting (town);

			//var plantsPerWorker = 10;

			//if (treesToPlant > plantsPerWorker)
			//	workersNeeded = 

			//var workersEngine = new WorkersEngine ();
			//workersEngine.Hire (town, workersNeeded, EmploymentType.Forestry, null);


		}

		public void HireWorkers(Town town)
		{
			var treesToPlant = town.TreesToPlantPerDay;

			var workersNeeded = treesToPlant;

			for (int i = 0; i < workersNeeded; i++) {
				var plant = new Plant (PlantType.Tree);
				plant.TimePlanted = Clock.GameDuration;
				plant.WasPlanted = true;

				Workers.Hire (town, 1, EmploymentType.Forestry, plant);

				if (plant.Workers.Length > 0) {
					var plants = new List<Plant> (town.Plants);
					plants.Add (plant);
					town.Plants = plants.ToArray ();
				}
			}
		}

		public void DoPlanting(Town town)
		{
			foreach (var person in town.People) {
				if (person.IsEmployed
				    && person.EmploymentType == EmploymentType.Forestry) {
					var plant = (Plant)person.EmploymentTarget;

					if (plant.PercentPlanted >= 100) {
						town.TotalTreesPlanted++;
						Workers.Fire (person);	
					} else {
						DoPlanting (plant);
					}
				}
			}
		}

		public void DoPlanting(Plant plant)
		{
			plant.PercentPlanted += GetPlantingCompletionIncrement ();

			if (plant.PercentPlanted > 100)
				plant.PercentPlanted = 100;
		}

		public double GetPlantingCompletionIncrement()
		{
			var increment = 100 / PlantingTimeCost;

			return increment;
		}
	}
}

