using System;
using townsim.Entities;

namespace townsim.Engine
{
	public class ThirstEngine
	{
		public double WaterConsumptionRate = 1;
		public double ThirstRate = 1.5;

		public ThirstEngine ()
		{
		}

		public void Update(Person person)
		{
			//foreach (var person in town.People) {
				UpdateThirst (person);
				UpdateWaterConsumption (person);
			//}
		}

		public void UpdateThirst(Person person)
		{
			person.Thirst += ThirstRate;

			if (person.Thirst > 100)
				person.Thirst = 100;
		}

		public void UpdateWaterConsumption(Person person)
		{
			var randomiser = new Random ().Next (200);

			var decider = randomiser < person.Thirst;

			if (decider) {
				var amountConsumed = person.Thirst * WaterConsumptionRate;
				person.Location.WaterSources -= amountConsumed;
				person.Thirst -= amountConsumed;
			}

			if (person.Thirst < 0)
				person.Thirst = 0;
		}
	}
}

