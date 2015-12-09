using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class HarvestActivity : BaseActivity
	{
		public WorkersUtility Workers = new WorkersUtility();

		public double HarvestingTimeCost = 2;

		public decimal FoodToPlantRatio = 0.5m;

		public HarvestActivity (EngineSettings settings, EngineClock clock) : base(settings, clock)
		{
		}

		public override void Act()
		{
			PerformHarvesting ();
		}

		public void PerformHarvesting()
		{
			var plant = (Plant)Person.ActivityTarget;

			if (plant == null)
				plant = AssignRipeVegetable (Person.Town, Person);

			if (plant != null) {
				if (plant.PercentHarvested >= 100) {
					Person.Town.TotalVegetablesHarvested++;
					Person.Town.FoodSources += ExtractFood (plant);
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

