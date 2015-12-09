using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class CollectWaterActivity : BaseActivity
	{
		public decimal CollectionRate = 50.0m;
		
		public CollectWaterActivity (EngineSettings settings) : base(settings)
		{
		}

		public override void Act()
		{
			if (Person.ActivityType == ActivityType.CollectingWater) {
				if (Person.Supplies [SupplyTypes.Water] < Person.SuppliesMax [SupplyTypes.Water]) {

					if (!Person.ActivityData.ContainsKey ("TotalWaterCollected")) {
						Person.ActivityData ["TotalWaterCollected"] = 0m;
					}
					
					var amount = CollectionRate;

					if (Person.Supplies [SupplyTypes.Water] >= Person.SuppliesMax [SupplyTypes.Water]) { // If water is full, stop collecting
						var total = (decimal)Person.ActivityData ["TotalWaterCollected"];
						LogWriter.Current.AppendLine (CurrentEngine.Id, "Collected " + total + " water.");
						Person.FinishActivity ();
					}
					else if (Person.Town.WaterSources > amount) { // If water is available in the town, collect it
						Person.Supplies [SupplyTypes.Water] += amount;
						Person.Town.WaterSources -= amount;

						Person.ActivityData ["TotalWaterCollected"] = (decimal)Person.ActivityData ["TotalWaterCollected"] + amount;
					}
				}
			}
		}
	}
}

