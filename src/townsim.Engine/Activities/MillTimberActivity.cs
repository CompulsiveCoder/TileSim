using System;
using townsim.Engine.Activities;
using townsim.Data;
using townsim.Engine.Entities;
using townsim.Engine.Needs;

namespace townsim.Engine.Activities
{
	[Serializable]
	[Activity(ItemType.Timber)]
	public class MillTimberActivity : BaseActivity
	{
		public decimal TotalTimberMilled = 0;

		public MillTimberActivity (Person person, NeedEntry needEntry, EngineSettings settings)
			: base(person, needEntry, settings)
		{
		}

		public override bool CheckFinished ()
		{
			return TotalTimberMilled >= NeedEntry.Quantity;
		}

		public override void Prepare (Person person)
		{
			throw new NotImplementedException ();
		}

		public override void Execute (Person person)
		{
			if (Settings.IsVerbose) {
				Console.WriteLine ("Starting " + GetType ().Name + " activity");
				Console.WriteLine ("  Quantity (timber): " + NeedEntry.Quantity);
				Console.WriteLine ("  Current (timber): " + person.Supplies[ItemType.Timber]);
			}

			// TODO: Clean up
			MillTimberCycle (person);

			//if (PersonIsBuildingTheirHomeAndNeedsTimber ()) {
				/*var amount = Person.GetDemandAmount(NeedType.Timber);

				MillTimberCycle (amount);*/
			//}
		}

		public bool PersonIsBuildingTheirHomeAndNeedsTimber()
		{
			throw new NotImplementedException ();
			return Actor.Home != null
				&& !Actor.Home.IsCompleted
				&& Actor.Home.TimberPending > 0;
		}

        public void MillTimberCycle(Person person)
		{
			// TODO: Remove if not needed
			/*if (Settings.PlayerId == Person.Id
			    && Settings.OutputType == ConsoleOutputType.General) {
				PlayerLog.WriteLine (CurrentEngine.Id, "The player has started milling timber.");
				PlayerLog.WriteLine (CurrentEngine.Id, "Timber needed: " + amountOfTimber);
			}*/

			var amountOfTimberToMillThisCycle = Settings.TimberMillingRate;

            if (NeedEntry.Quantity < amountOfTimberToMillThisCycle)
                amountOfTimberToMillThisCycle = NeedEntry.Quantity;

			ConvertWoodToTimber (person, amountOfTimberToMillThisCycle);
		}

        public override bool CheckSupplies(Person actor)
		{
            if (!HasEnoughWood (NeedEntry.Quantity)) {
                RegisterNeedForWood (Actor, NeedEntry.Quantity);

                return false;
            } else
                return true;
		}
        public void RegisterNeedForWood(Person person, decimal quantity)
		{

			var amountOfWoodNeeded = CalculateAmountOfWoodNeeded (NeedEntry.Quantity);

			if (Settings.IsVerbose)
				Console.WriteLine ("  Registering the need for " + amountOfWoodNeeded + " wood");

			person.AddNeed (ItemType.Wood, amountOfWoodNeeded, 102); // TODO: Figure out a better way to decide priority
		}

		public void GetWood(decimal woodRequired)
		{
			throw new NotImplementedException ();
			//var fellWoodActivity = new FellWoodActivity (Actor, Settings);

			//if (Settings.IsVerbose) {
			//	Console.WriteLine ("        Getting " + woodRequired + " wood by starting " + fellWoodActivity.GetType().Name + " activity.");
			//}
			
			//fellWoodActivity.SetQuantity (woodRequired);
			//Actor.RushActivity (fellWoodActivity);
		}

		public void ConvertWoodToTimber(Person actor, decimal amountOfTimber)
		{
            if (Settings.IsVerbose)
                Console.WriteLine ("  Converting wood to timber");
            
			var woodNeeded = CalculateAmountOfWoodNeeded(amountOfTimber);

			// TODO: Is there a cleaner way to do this?
            if (actor.Supplies [ItemType.Wood] >= woodNeeded) {
                TotalTimberMilled += amountOfTimber;
                if (Settings.IsVerbose) {
                    Console.WriteLine ("    Wood: " + woodNeeded);
                    Console.WriteLine ("    Timber: " + amountOfTimber);
                    Console.WriteLine ("    Total timber: " + TotalTimberMilled);
                }
                NeedsConsumed[ItemType.Wood] += woodNeeded;
                NeedsProduced[ItemType.Timber] += amountOfTimber;
                //DemandsResolved.Add(NeedType.Timber, // TODO: Remove if not needed. Should be obsolete because the need is removed when the activity is finished.
                //actor.RemoveDemand (NeedType.Timber, amountOfTimber);
            } else {
                if (Settings.IsVerbose) {
                    Console.WriteLine ("    Not enough wood: " + woodNeeded);
                }
            }

			/*if (Settings.PlayerId == Person.Id
				&& Settings.OutputType == ConsoleOutputType.General) {
				PlayerLog.WriteLine (CurrentEngine.Id, "The player has started milling timber.");
				PlayerLog.WriteLine (CurrentEngine.Id, TotalTimberMilled + " timber");
			}*/

			// TODO: Remove. Obsolete
			//if (IsComplete)
			//	Finish ();
		}

		public bool CheckComplete ()
		{
			throw new NotImplementedException ();
			/*var value = !Person.HasDemand (NeedType.Timber);
			return value;*/
		}

		public bool CheckImpossible ()
		{
			throw new NotImplementedException ();
			/*var amount = Person.GetDemandAmount (NeedType.Timber);

			var woodNeeded = amount * Settings.TimberWasteRate;

			var isImpossible = !Person.Has (NeedType.Wood, woodNeeded);

			return isImpossible;*/
		}

		/*public bool HasCompletedMillingTimber(decimal amountOfTimber)
		{
			return IsComplete ();
		}*/

		public bool HasEnoughWood(decimal amountOfWoodNeeded)
		{	
			var value = Actor.Has (ItemType.Wood, amountOfWoodNeeded);

			return value;
		}

		public decimal CalculateAmountOfWoodNeeded(decimal amountOfTimber)
		{
			return amountOfTimber * Settings.WoodRequiredForTimber;
		}
	}
}

