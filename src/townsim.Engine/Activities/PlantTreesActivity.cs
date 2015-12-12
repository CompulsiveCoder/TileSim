using System;
using townsim.Entities;
using System.Collections.Generic;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class PlantTreesActivity : BaseActivity
	{
		//public WorkersUtility Workers = new WorkersUtility();

		public decimal PlantingTimeCost = 2;

		public PlantTreesActivity (Person person, EngineSettings settings, EngineClock clock) : base(person, settings, clock)
		{
		}

		public override void ExecuteSingleCycle()
		{
			//throw new NotImplementedException ();
			/*var treesPlantedToday = town.CountTreesPlantedToday (Clock.GameDuration);

			if (treesPlantedToday < town.TreesToPlantPerDay)
				HireWorkers (town);

			DoPlanting (town);*/
		}

		public void HireWorkers(Town town)
		{
			if (town.TotalInactive > 0) {
				var treesToPlant = town.TreesToPlantPerDay;

				var workersNeeded = treesToPlant;

				for (int i = 0; i < workersNeeded; i++) {
					var plant = new Plant (PlantType.Tree);
					plant.TimePlanted = Clock.GameDuration;
					plant.WasPlanted = true;

					//Workers.Hire (town, 1, ActivityType.Forestry, plant);

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
				if (person.IsActive
				    && person.ActivityType == ActivityType.Forestry) {
					var plant = (Plant)person.ActivityTarget;

					if (plant != null) {
						if (plant.PercentPlanted >= 100) {
							town.TotalTreesPlanted++;
							//Workers.Fire (person);	

							LogWriter.Current.AppendLine (CurrentEngine.Id, "A tree has been planted.");
						} else {
							DoPlanting (plant);
						}
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

		public decimal GetPlantingCompletionIncrement()
		{
			var increment = 100 / PlantingTimeCost;

			return increment;
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

