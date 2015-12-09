using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine
{
	public class DrinkActivity
	{
		public decimal WaterConsumptionFactor = 0.05m; // liters
		public decimal ThirstSatisfactionRate = 1; // The rate at which thirst is reduced

		public EngineSettings Settings;

		public DrinkActivity (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Person person)
		{
			if (person.Activity == ActivityType.Drinking) {
				//var amountOfWaterRequired = person.Thirst;

				//var amountConsumed = amountOfWaterRequired * WaterConsumptionRate;

				var amountConsumed = person.Thirst * WaterConsumptionFactor / ThirstSatisfactionRate ;
				if (person.Location.WaterSources > 0) {
					if (amountConsumed > person.Supplies[SupplyTypes.Water])
						amountConsumed = person.Supplies[SupplyTypes.Water];
					if (amountConsumed > person.Thirst)
						amountConsumed = person.Thirst;

					if (CurrentEngine.PlayerId == person.Id)
						LogWriter.Current.AppendLine (CurrentEngine.Id, "Player consumed " + Convert.ToInt32 (amountConsumed) + "ml water.");

					person.Supplies[SupplyTypes.Water] -= amountConsumed;
					person.Thirst -= amountConsumed * ThirstSatisfactionRate;
				}

				if (person.Thirst < 0) {
					person.Thirst = 0;

					person.Priorities [PriorityTypes.Water] = 0;
				}
			}
		}
	}
}

