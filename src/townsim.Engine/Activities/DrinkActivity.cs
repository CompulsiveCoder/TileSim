using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class DrinkActivity : BaseActivityOld
	{
		public decimal WaterConsumptionFactor = 0.05m; // liters
		public decimal ThirstSatisfactionRate = 1; // The rate at which thirst is reduced

		public DrinkActivity (Person person, EngineContext context)
			: base(ActivityType.Drinking, person, context)
		{
		}

		protected override void ExecuteSingleCycle()
		{
			throw new NotImplementedException ();
			/*if (Person.ActivityType == ActivityType.Drinking)
			{
				if (Person.Thirst == 0 && Person.ActivityData.ContainsKey("TotalConsumed")) {
					if (Settings.PlayerId == Person.Id)
						PlayerLog.WriteLine (CurrentEngine.Id, "Player consumed " + Convert.ToInt32 (Person.ActivityData ["TotalConsumed"]) + "ml water.");

					Finish ();
				} else {
					if (!Person.ActivityData.ContainsKey ("TotalConsumed")) {
						Person.ActivityData.Add ("TotalConsumed", 0m);
						if (Settings.PlayerId == Person.Id)
							PlayerLog.WriteLine (CurrentEngine.Id, "Player started drinking water.");
					}

					decimal amountConsumed = Person.Thirst * WaterConsumptionFactor / ThirstSatisfactionRate;
					if (Person.Supplies [SupplyTypes.Water] > 0) {
						if (amountConsumed > Person.Supplies [SupplyTypes.Water])
							amountConsumed = Person.Supplies [SupplyTypes.Water];
						if (amountConsumed > Person.Thirst)
							amountConsumed = Person.Thirst;

						Person.Supplies [SupplyTypes.Water] = Person.Supplies [SupplyTypes.Water] - amountConsumed;
						Person.Thirst -= amountConsumed * ThirstSatisfactionRate;
						Person.ActivityData ["TotalConsumed"] = (decimal)Person.ActivityData ["TotalConsumed"] + amountConsumed;
					}

					if (Person.Thirst < 0) {
						Person.Thirst = 0;
					}
				}
			}*/
		}

		public override void Start ()
		{
			throw new NotImplementedException ();
		}

		public override bool CheckComplete ()
		{
			throw new NotImplementedException ();
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

