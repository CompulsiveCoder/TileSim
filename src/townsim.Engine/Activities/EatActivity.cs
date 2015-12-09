using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class EatActivity : BaseActivity
	{
		public decimal FoodConsumptionRate = 0.7m; // kgs
		public decimal FoodSatisfactionRate = 1; // The rate at which hunger is reduced upon consumption

		public EatActivity (EngineSettings settings)
		{
			Settings = settings;
		}

		public override void Act()
		{
			if (Person.ActivityType == ActivityType.Eating) {
				var amountOfFoodRequired = Person.Hunger;
				var amountConsumed = amountOfFoodRequired * FoodConsumptionRate;

				if (Person.Supplies[SupplyTypes.Food] >= 0) {
					
					if (amountConsumed > Person.Supplies[SupplyTypes.Food])
						amountConsumed = Person.Supplies[SupplyTypes.Food];
					if (amountConsumed > Person.Hunger)
						amountConsumed = Person.Hunger;

					if (CurrentEngine.PlayerId == Person.Id)
						LogWriter.Current.AppendLine (CurrentEngine.Id, "Player ate " + (int)amountConsumed + "grams of food.");

					Person.Supplies[SupplyTypes.Food] -= amountConsumed;
					Person.Hunger -= amountConsumed * FoodSatisfactionRate;

					
				}

				if (Person.Hunger <= 0)
				{
					Person.Hunger = 0;
					Person.ActivityType = ActivityType.Inactive;
				}
			}
		}
	}
}

