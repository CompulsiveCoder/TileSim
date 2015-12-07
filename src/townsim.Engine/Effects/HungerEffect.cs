using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Effects
{
	public class HungerEffect
	{
		public decimal FoodConsumptionRate = 0.7m; // kgs
		public decimal FoodSatisfactionRate = 1; // The rate at which hunger is reduced upon consumption

		public decimal HungerRate = 100m / (24*60*60) * 3m; // 100% / seconds in a day * meals per day

		public EngineSettings Settings { get;set; }

		public HungerEffect (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Person person)
		{
			if (person.IsAlive) {
				UpdateHunger (person);
				UpdateFoodConsumption (person);
			}
		}

		public void UpdateHunger(Person person)
		{
			var increase = HungerRate * Settings.GameSpeed;

			person.Hunger += increase;

			if (person.Hunger > 100)
				person.Hunger = 100;
		}

		public void UpdateFoodConsumption(Person person)
		{
			// TODO: Turn this into an activity
			var randomiser = new Random ().Next (400);

			var willEat = randomiser < person.Hunger;

			if (person.Hunger >= 99)
				willEat = true;

			if (willEat) {
				var amountOfFoodRequired = person.Hunger;
				var amountConsumed = amountOfFoodRequired * FoodConsumptionRate * Settings.GameSpeed;
				if (person.Location.FoodSources >= 0) {
					if (amountConsumed > person.Location.FoodSources)
						amountConsumed = person.Location.FoodSources;
					if (amountConsumed > person.Hunger)
						amountConsumed = person.Hunger;

          if (CurrentEngine.PlayerId == person.Id)
            LogWriter.Current.AppendLine (CurrentEngine.Id, "Player ate " + (int)amountConsumed + "grams of food.");

					person.Location.FoodSources -= amountConsumed;
					person.Hunger -= amountConsumed * FoodSatisfactionRate;
				}
			}
		}
	}
}

