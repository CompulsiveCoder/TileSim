using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	public class CollectWaterActivity : BaseActivity
	{
		public decimal CollectionRate = 50.0m;
		
		public CollectWaterActivity (EngineSettings settings) : base(settings)
		{
		}

		public void Update(Person person)
		{
			if (person.Activity == ActivityType.CollectingWater) {
				if (person.Supplies [SupplyTypes.Water] < person.SuppliesMax [SupplyTypes.Water]) {

					if (!person.ActivityData.ContainsKey ("TotalWaterCollected")) {
 						person.ActivityData ["TotalWaterCollected"] = 0m;
					}
					
					var amount = CollectionRate;

					if (person.Supplies [SupplyTypes.Water] >= person.SuppliesMax [SupplyTypes.Water]) { // If water is full, stop collecting
						var total = (decimal)person.ActivityData ["TotalWaterCollected"];
						LogWriter.Current.AppendLine (CurrentEngine.Id, "Collected " + total + " water.");
						person.Finish ();
					}
					else if (person.Town.WaterSources > amount) { // If water is available in the town, collect it
						person.Supplies [SupplyTypes.Water] += amount;
						person.Town.WaterSources -= amount;

						person.ActivityData ["TotalWaterCollected"] = (decimal)person.ActivityData ["TotalWaterCollected"] + amount;
					}
				}
			}
		}
	}
}

