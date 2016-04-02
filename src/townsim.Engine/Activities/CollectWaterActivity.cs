using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class CollectWaterActivity : BaseActivityOld
	{
		public decimal CollectionRate = 50.0m;
		
		public CollectWaterActivity (Person person, EngineContext context)
			: base(ActivityType.CollectingWater, person, context)
		{
		}

		protected override void ExecuteSingleCycle()
		{
			if (Person.ActivityType == ActivityType.CollectingWater) {
				if (Person.Supplies [SupplyTypes.Water] < Person.SuppliesMax [SupplyTypes.Water]) {

					if (!Person.ActivityData.ContainsKey ("TotalWaterCollected")) {
						Person.ActivityData ["TotalWaterCollected"] = 0m;
					}
					
					var amount = CollectionRate;

					if (IsComplete) { // If water is full, stop collecting
						var total = (decimal)Person.ActivityData ["TotalWaterCollected"];
						throw new NotImplementedException ();
						//PlayerLog.WriteLine (CurrentEngine.Id, "Collected " + total + " water.");
						//Finish ();
					}
					else if (Person.Town.WaterSources > amount) { // If water is available in the town, collect it
						Person.Supplies [SupplyTypes.Water] += amount;
						Person.Town.WaterSources -= amount;

						Person.ActivityData ["TotalWaterCollected"] = (decimal)Person.ActivityData ["TotalWaterCollected"] + amount;
					}
				}
			}
		}

		public override void Start ()
		{
			throw new NotImplementedException ();
		}

		public override bool CheckComplete ()
		{
			return Person.Supplies [SupplyTypes.Water] >= Person.SuppliesMax [SupplyTypes.Water];
		}

		public override bool CheckImpossible ()
		{
			throw new NotImplementedException ();
		}

		public override void Finish ()
		{
			throw new NotImplementedException ();
		}
	}
}