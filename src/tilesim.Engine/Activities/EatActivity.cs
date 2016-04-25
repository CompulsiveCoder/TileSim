using System;
using tilesim.Entities;
using tilesim.Data;

namespace tilesim.Engine.Activities
{
	[Serializable]
	public class EatActivity : BaseActivityOld
	{
		public decimal FoodConsumptionRate = 0.7m; // kgs
		public decimal FoodSatisfactionRate = 1; // The rate at which hunger is reduced upon consumption

		public EatActivity (Person person, EngineContext context)
			: base(ActivityType.Eating, person, context)
		{
		}

		protected override void ExecuteSingleCycle()
		{
			if (Person.ActivityType == ActivityType.Eating) {
				var amountOfFoodRequired = Person.Hunger;
				var amountConsumed = amountOfFoodRequired * FoodConsumptionRate;

				if (Person.Supplies[SupplyTypes.Food] >= 0) {
					
					if (amountConsumed > Person.Supplies[SupplyTypes.Food])
						amountConsumed = Person.Supplies[SupplyTypes.Food];
					if (amountConsumed > Person.Hunger)
						amountConsumed = Person.Hunger;

					throw new NotImplementedException ();
					//if (Settings.PlayerId == Person.Id)
					//	PlayerLog.WriteLine (CurrentEngine.Id, "Player ate " + (int)amountConsumed + "grams of food.");

					Person.Supplies[SupplyTypes.Food] -= amountConsumed;
					Person.Hunger -= amountConsumed * FoodSatisfactionRate;

					
				}

				if (Person.Hunger <= 0)
				{
					Person.Hunger = 0;
					Finish ();
				}
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

