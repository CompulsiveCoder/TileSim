using System;
using townsim.Engine.Activities;
using townsim.Data;
using townsim.Entities;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class MillTimberActivity : BaseActivity
	{
		public decimal MillRate = 10;

		public decimal TotalTimberMilled = 0;

		public MillTimberActivity (Person person, EngineSettings settings) : base(person, settings)
		{
		}

		public override void ExecuteSingleCycle ()
		{
			if (PersonIsBuildingTheirHomeAndNeedsTimber ()) {
				var amount = Person.GetDemandAmount(SupplyTypes.Timber);

				MillTimberCycle (amount);
			}
		}

		public bool PersonIsBuildingTheirHomeAndNeedsTimber()
		{
			return Person.Home != null
			&& !Person.Home.IsCompleted
			&& Person.Home.TimberPending > 0;
		}

		public void MillTimberCycle(decimal amountOfTimber)
		{
			if (CurrentEngine.PlayerId == Person.Id
			    && Settings.OutputType == ConsoleOutputType.General) {
				LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started milling timber.");
				LogWriter.Current.AppendLine (CurrentEngine.Id, "Timber needed: " + amountOfTimber);
			}

			if (HasCompletedMillingTimber(amountOfTimber))
				Finish ();
			else
			{
				var amountOfTimberToMillThisCycle = MillRate;

				var totalWoodNeededThisCycle = CalculateAmountOfWoodNeeded (amountOfTimberToMillThisCycle);

				if (HasEnoughWood(totalWoodNeededThisCycle)) {
					ConvertWoodToTimber (amountOfTimberToMillThisCycle);
				} else
					throw new Exception ("Not enough wood available."); // TODO: Should a demand for wood be added here?
			}
				
		}

		public void ConvertWoodToTimber(decimal amountOfTimber)
		{
			var woodNeeded = CalculateAmountOfWoodNeeded(amountOfTimber);

			// TODO: Is there a cleaner way to do this?
			if (Person.Supplies [SupplyTypes.Wood] >= woodNeeded) {
				Person.Supplies [SupplyTypes.Wood] -= woodNeeded;
				Person.Supplies [SupplyTypes.Timber] += amountOfTimber;
				Person.RemoveDemand (SupplyTypes.Timber, amountOfTimber);
				TotalTimberMilled += amountOfTimber;
			}

			if (CurrentEngine.PlayerId == Person.Id
				&& Settings.OutputType == ConsoleOutputType.General) {
				LogWriter.Current.AppendLine (CurrentEngine.Id, "The player has started milling timber.");
				LogWriter.Current.AppendLine (CurrentEngine.Id, TotalTimberMilled + " timber");
			}

			if (IsComplete ())
				Finish ();
		}

		public override void Start ()
		{
		}

		public override bool IsComplete ()
		{
			var value = !Person.HasDemand (SupplyTypes.Timber);
			return value;
		}

		public override bool IsImpossible ()
		{
			var amount = Person.GetDemandAmount (SupplyTypes.Timber);

			var woodNeeded = amount * Settings.TimberWasteRate;

			var isImpossible = !Person.Has (SupplyTypes.Wood, woodNeeded);

			return isImpossible;
		}

		public override void Finish ()
		{
			base.CleanUp ();
		}

		public bool HasCompletedMillingTimber(decimal amountOfTimber)
		{
			return IsComplete ();
		}

		public bool HasEnoughWood(decimal amountOfWoodNeeded)
		{	
			var value = Person.Has (SupplyTypes.Wood, amountOfWoodNeeded);

			return value;
		}

		public decimal CalculateAmountOfWoodNeeded(decimal amountOfTimber)
		{
			return amountOfTimber * Settings.TimberWasteRate;
		}
	}
}

