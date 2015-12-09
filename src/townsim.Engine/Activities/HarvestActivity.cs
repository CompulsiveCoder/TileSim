using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	public class HarvestActivity : BaseActivity
	{
		public WorkersUtility Workers = new WorkersUtility();

		public double HarvestingTimeCost = 2;

		public decimal FoodToPlantRatio = 0.5m;

		public HarvestActivity (EngineSettings settings, EngineClock clock) : base(settings, clock)
		{
		}

		public void Update(Town town)
		{
			//var vegetablesHarvestedToday = town.CountVegetablesHarvestedToday (Clock.GameDuration);

			//if (vegetablesHarvestedToday < town.VegetablesToHarvestPerDay)
			//	HireWorkers (town);

			PerformHarvesting (town);
		}

		// TODO: Remove if not needed
		public void HireWorkers(Town town)
		{
			if (town.TotalInactive > 0) {
				if (town.RipeVegetables.Length > 0) {
					var vegetablesToHarvest = town.VegetablesToHarvestPerDay;

					var workersNeeded = vegetablesToHarvest;

					for (int i = 0; i < workersNeeded; i++) {
						var plant = new Plant (PlantType.Vegetable);
						plant.TimeHarvested = Clock.GameDuration;
						plant.WasHarvested = true;
					}
				}
			}
		}

		public void PerformHarvesting(Town town)
		{
			foreach (var person in town.People) {
				if (person.Activity == ActivityType.Harvesting) {
					PerformHarvesting (town, person);
				}
			}
		}

		public void PerformHarvesting(Town town, Person person)
		{
			var plant = (Plant)person.ActivityTarget;

			if (plant == null)
				plant = AssignRipeVegetable (town, person);

			if (plant != null) {
				if (plant.PercentHarvested >= 100) {
					town.TotalVegetablesHarvested++;
					town.FoodSources += ExtractFood (plant);
					//Workers.Fire (person);	

					LogWriter.Current.AppendLine (CurrentEngine.Id, "A vegetable has been harvested.");
				} else {
					PerformHarvesting (plant);
				}
			}
		}

		public Plant AssignRipeVegetable(Town town, Person person)
		{
			if (town.RipeVegetables.Length > 0) {
				var vegetable = town.FindRipeUnassignedVegetable ();
				person.FocusOn (vegetable);
				return vegetable;
			} else
				LogWriter.Current.AppendLine (CurrentEngine.Id, "Not enough ripe vegetables available.");

			return null;
		}


		public void PerformHarvesting(Plant plant)
		{
			plant.PercentHarvested += GetHarvestingCompletionIncrement ();

			if (plant.PercentHarvested > 100)
				plant.PercentHarvested = 100;
		}

		public double GetHarvestingCompletionIncrement()
		{
			var increment = 100 / HarvestingTimeCost;

			return increment;
		}

		public decimal ExtractFood(Plant plant)
		{
			// TODO: Adjust the amount of food from each plant
			return (decimal)plant.Size*FoodToPlantRatio;
		}
	}
}

