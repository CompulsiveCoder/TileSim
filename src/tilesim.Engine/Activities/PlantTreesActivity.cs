using System;
using tilesim.Entities;
using System.Collections.Generic;
using tilesim.Data;

namespace tilesim.Engine.Activities
{
	[Serializable]
	public class PlantTreesActivity : BaseActivityOld
	{
		//public WorkersUtility Workers = new WorkersUtility();

		public decimal PlantingTimeCost = 2;

		public PlantTreesActivity (Person person, EngineContext context)
			: base(ActivityType.PlantTrees, person, context)
		{
		}

		protected override void ExecuteSingleCycle()
		{
			//throw new NotImplementedException ();
			/*var treesPlantedToday = tile.CountTreesPlantedToday (Clock.GameDuration);

			if (treesPlantedToday < tile.TreesToPlantPerDay)
				HireWorkers (tile);

			DoPlanting (tile);*/
		}

		public void HireWorkers(Tile tile)
		{
			if (tile.TotalInactive > 0) {
				var treesToPlant = tile.TreesToPlantPerDay;

				var workersNeeded = treesToPlant;

				for (int i = 0; i < workersNeeded; i++) {
					var plant = new Plant (PlantType.Tree);
					throw new NotImplementedException ();
					/*
					plant.TimePlanted = Clock.GameDuration;
					plant.WasPlanted = true;

					//Workers.Hire (tile, 1, ActivityType.Forestry, plant);

					if (plant.People.Length > 0) {
						var plants = new List<Plant> (tile.Plants);
						plants.Add (plant);
						tile.Plants = plants.ToArray ();
					}*/
				}
			}
		}

		public void DoPlanting(Tile tile)
		{
			foreach (var person in tile.People) {
				if (person.IsActive
				    && person.ActivityType == ActivityType.Forestry) {
					var plant = (Plant)person.Activity.Target;

					if (plant != null) {
						if (plant.PercentPlanted >= 100) {
							tile.TotalTreesPlanted++;
							//Workers.Fire (person);	

							throw new NotImplementedException ();
							//PlayerLog.WriteLine (CurrentEngine.Id, "A tree has been planted.");
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

		public override bool CheckComplete ()
		{
			throw new NotImplementedException ();
		}

		public override bool CheckImpossible ()
		{
			throw new NotImplementedException ();
		}

		public override void Finish ()
		{
			throw new NotImplementedException ();
		}
	}
}

