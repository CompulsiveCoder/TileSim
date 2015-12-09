using System;
using townsim.Entities;

namespace townsim.Engine.Activities
{
	public class CollectWaterActivity : BaseActivity
	{
		public decimal CollectionRate = 0.1m;
		
		public CollectWaterActivity (EngineSettings settings) : base(settings)
		{
		}

		public void Update(Person person)
		{
			if (person.ActivityType == ActivityType.CollectingWater) {
				if (person.Supplies [SupplyTypes.Water] < person.SuppliesMax [SupplyTypes.Water]) {
					var amount = CollectionRate;

					if (person.Town.WaterSources > amount) {
						person.Supplies [SupplyTypes.Water] += amount;
						person.Town.WaterSources -= amount;
					}
				}
			}
		}
	}
}

