using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class GardenActivity : BaseActivity
	{
		public WorkersUtility Workers = new WorkersUtility();

		public double PlantingIncrement = 100;

		public GardenActivity (EngineSettings settings, EngineClock clock) : base(settings, clock)
		{
		}

		public override void Act()
		{
			DoPlanting ();
		}

		public void DoPlanting()
		{
			if (Person.ActivityType == ActivityType.Gardening) {
				var town = Person.Town;

				var plant = (Plant)Person.ActivityTarget;

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
					Person.FinishActivity ();

					LogWriter.Current.AppendLine (CurrentEngine.Id, "A vegetable seedling has been planted.");
				} else {
					DoPlanting (plant);
				}
			}
		}

		public void DoPlanting(Plant plant)
		{
			if (plant.PercentPlanted < 100) {
				plant.PercentPlanted += PlantingIncrement;
			}

			if (plant.PercentPlanted >= 100) {
				plant.PercentPlanted = 100;
				plant.WasPlanted = true;
			}
		}
	}
}

