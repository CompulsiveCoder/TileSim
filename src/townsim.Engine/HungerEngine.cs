using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class HungerEngine
	{
		public double FoodConsumptionRate = 0.3;
		public double HungerRate = 1;

		public HungerEngine ()
		{
		}

		public void Update(Person person)
		{
			//foreach (var person in town.People) {
				UpdateHunger (person);
				UpdateFoodConsumption (person);
			//}
		}

		public void UpdateHunger(Person person)
		{
			person.Hunger += HungerRate;
		}

		public void UpdateFoodConsumption(Person person)
		{
			var randomiser = new Random ().Next (400);

			var willEat = randomiser < person.Hunger;


			if (willEat) {
				var amountConsumed = person.Hunger * FoodConsumptionRate;
				if (person.Location.FoodSources == 0) {
					person.Health -= 5;
				} else {	
					if (amountConsumed > person.Location.FoodSources)
						amountConsumed = person.Location.FoodSources;
					person.Location.FoodSources -= amountConsumed;
					person.Hunger -= amountConsumed;
				}
			}
		}
	}
}

