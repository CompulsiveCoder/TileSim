using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	public class DrinkActivity : BaseActivity
	{
		public decimal WaterConsumptionFactor = 0.05m; // liters
		public decimal ThirstSatisfactionRate = 1; // The rate at which thirst is reduced

		public DrinkActivity (EngineSettings settings)
		{
			Settings = settings;
		}

		public void Update(Person person)
		{
			if (person.Activity == ActivityType.Drinking)
			{
				if (person.Thirst == 0 && person.ActivityData.ContainsKey("TotalConsumed")) {
					if (CurrentEngine.PlayerId == person.Id)
						LogWriter.Current.AppendLine (CurrentEngine.Id, "Player consumed " + Convert.ToInt32 (person.ActivityData ["TotalConsumed"]) + "ml water.");

					person.Finish ();
				} else {
					//var amountOfWaterRequired = person.Thirst;

					//var amountConsumed = amountOfWaterRequired * WaterConsumptionRate;

					if (!person.ActivityData.ContainsKey ("TotalConsumed")) {
						person.ActivityData.Add ("TotalConsumed", 0m);
						if (CurrentEngine.PlayerId == person.Id)
							LogWriter.Current.AppendLine (CurrentEngine.Id, "Player started drinking water.");
					}

					decimal amountConsumed = person.Thirst * WaterConsumptionFactor / ThirstSatisfactionRate;
					if (person.Supplies [SupplyTypes.Water] > 0) {
						if (amountConsumed > person.Supplies [SupplyTypes.Water])
							amountConsumed = person.Supplies [SupplyTypes.Water];
						if (amountConsumed > person.Thirst)
							amountConsumed = person.Thirst;

						person.Supplies [SupplyTypes.Water] = person.Supplies [SupplyTypes.Water] - amountConsumed;
						person.Thirst -= amountConsumed * ThirstSatisfactionRate;
						person.ActivityData ["TotalConsumed"] = (decimal)person.ActivityData ["TotalConsumed"] + amountConsumed;
					}

					if (person.Thirst < 0) {
						person.Thirst = 0;
					}
				}
			}
		}
	}
}

