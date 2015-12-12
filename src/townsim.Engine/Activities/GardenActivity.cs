using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class GardenActivity : BaseActivity
	{
		//public WorkersUtility Workers = new WorkersUtility();

		public decimal PlantingIncrement = 100;

		public GardenActivity (Person person, EngineSettings settings, EngineClock clock) : base(person, settings, clock)
		{
		}

		public override void ExecuteSingleCycle()
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

		public override void Start ()
		{
			throw new NotImplementedException ();
		}

		public override bool IsComplete ()
		{
			throw new NotImplementedException ();
		}

		public override bool IsImpossible ()
		{
			throw new NotImplementedException ();
		}

		public override void Finish ()
		{
			throw new NotImplementedException ();
		}
	}
}

