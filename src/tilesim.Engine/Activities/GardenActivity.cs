using System;
using tilesim.Entities;
using System.Collections.Generic;
using tilesim.Data;

namespace tilesim.Engine.Activities
{
	[Serializable]
	public class GardenActivity : BaseActivityOld
	{
		//public WorkersUtility Workers = new WorkersUtility();

		public decimal PlantingIncrement = 100;

		public GardenActivity (Person person, EngineContext context)
			: base(ActivityType.Gardening, person, context)
		{
		}

		protected override void ExecuteSingleCycle()
		{
			DoPlanting ();
		}

		public void DoPlanting()
		{
			if (Person.Activity.Type == ActivityType.Gardening) {
				var tile = Person.Tile;

				var plant = (Plant)Person.Activity.Target;

				if (plant == null) {
					plant = new Plant (PlantType.Vegetable);

					throw new NotImplementedException ();
					//plant.TimePlanted = Clock.GameDuration;
					plant.WasPlanted = true;

					var plants = new List<Plant> (tile.Plants);
					plants.Add (plant);
					tile.Plants = plants.ToArray ();
				}

				DoPlanting (plant);

				if (plant.PercentPlanted >= 100) {
					tile.TotalVegetablesPlanted++;
					Finish();

					throw new NotImplementedException ();
					//PlayerLog.WriteLine (CurrentEngine.Id, "A vegetable seedling has been planted.");
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

