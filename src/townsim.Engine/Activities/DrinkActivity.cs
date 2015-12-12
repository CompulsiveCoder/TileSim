using System;
using townsim.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class DrinkActivity : BaseActivity
	{
		public decimal WaterConsumptionFactor = 0.05m; // liters
		public decimal ThirstSatisfactionRate = 1; // The rate at which thirst is reduced

		public DrinkActivity (Person person, EngineSettings settings) : base(person, settings)
		{
		}

		public override void ExecuteSingleCycle()
		{
			if (Person.ActivityType == ActivityType.Drinking)
			{
				if (Person.Thirst == 0 && Person.ActivityData.ContainsKey("TotalConsumed")) {
					if (CurrentEngine.PlayerId == Person.Id)
						LogWriter.Current.AppendLine (CurrentEngine.Id, "Player consumed " + Convert.ToInt32 (Person.ActivityData ["TotalConsumed"]) + "ml water.");

					Person.FinishActivity ();
				} else {
					if (!Person.ActivityData.ContainsKey ("TotalConsumed")) {
						Person.ActivityData.Add ("TotalConsumed", 0m);
						if (CurrentEngine.PlayerId == Person.Id)
							LogWriter.Current.AppendLine (CurrentEngine.Id, "Player started drinking water.");
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
			}
		}

		public override void Start ()
		{
			throw new NotImplementedException ();
		}

		public override bool IsComplete ()
		{
			throw new NotImplementedException ();
		}

		public override bool IsImpossible ()
		{
			throw new NotImplementedException ();
		}

		public override void Finish ()
		{
			throw new NotImplementedException ();
		}
	}
}

