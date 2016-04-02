using System;
using townsim.Entities;
using townsim.Engine.Activities;
using townsim.Data;

namespace townsim.Engine.Decisions
{
	public class TimberDecision : BaseDecision
	{
		public TimberDecision (EngineContext context) : base(context)
		{
		}

		public override void Decide(Person person)
		{
			throw new NotImplementedException ();
			/*if (person.ActivityType != ActivityType.MillTimber) {
				if (HasDemandForTimber (person)) {
					var amount = person.GetDemandAmount (SupplyTypes.Timber);

					var woodNeeded = CalculateAmountOfWoodNeeded (amount);

					if (!HasEnoughWood (person, woodNeeded)) {

						if (Context.Settings.OutputType == ConsoleOutputType.Debug
							&& Context.Settings.PlayerId == person.Id) {
							Console.WriteLine ("Can't fulfil demand for timber. Not enough wood to mill. Adding demand for wood.");
						}

						AddDemandForWood (person, woodNeeded);
					} else {
						StartMillingTimber (person);
					}
				}
			}

			return person.ActivityType;*/
		}

		public void AddDemandForWood(Person person, decimal amountOfWood)
		{
			var woodRemaining = amountOfWood - person.Supplies [SupplyTypes.Wood];

			person.AddDemand (SupplyTypes.Wood, woodRemaining);
		}

		public void StartMillingTimber(Person person)
		{
			if (Context.Settings.OutputType == ConsoleOutputType.Debug
				&& Context.Settings.PlayerId == person.Id) {
				Context.Log.WriteLine ("Starting to mill timber.");
			}

			person.Assign (ActivityType.MillTimber);
		}

		public bool HasEnoughWood(Person person, decimal amountOfWood)
		{
			return person.Supplies [SupplyTypes.Wood] >= amountOfWood;
		}

		public bool HasDemandForTimber(Person person)
		{
			return person.HasDemand (SupplyTypes.Timber);
		}

		public decimal CalculateAmountOfWoodNeeded(decimal amountOfTimber)
		{
			return amountOfTimber * Context.Settings.TimberWasteRate;
		}
	}
}

