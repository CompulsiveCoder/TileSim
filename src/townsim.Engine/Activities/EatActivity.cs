using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	public class EatActivity : BaseActivity
	{
		public decimal FoodConsumptionRate = 0.7m; // kgs
		public decimal FoodSatisfactionRate = 1; // The rate at which hunger is reduced upon consumption

		public EngineSettings Settings;

		public EatActivity (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Person person)
		{
			if (person.Activity == ActivityType.Eating) {
				var amountOfFoodRequired = person.Hunger;
				var amountConsumed = amountOfFoodRequired * FoodConsumptionRate;

				if (person.Supplies[SupplyTypes.Food] >= 0) {
					
					if (amountConsumed > person.Supplies[SupplyTypes.Food])
						amountConsumed = person.Supplies[SupplyTypes.Food];
					if (amountConsumed > person.Hunger)
						amountConsumed = person.Hunger;

					if (CurrentEngine.PlayerId == person.Id)
						LogWriter.Current.AppendLine (CurrentEngine.Id, "Player ate " + (int)amountConsumed + "grams of food.");

					person.Supplies[SupplyTypes.Food] -= amountConsumed;
					person.Hunger -= amountConsumed * FoodSatisfactionRate;

					
				}

				if (person.Hunger <= 0)
				{
					person.Hunger = 0;
					person.Activity = ActivityType.Inactive;
				}
			}
		}
	}
}

