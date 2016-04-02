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
				Console.WriteLine ("  Current (timber): " + person.Inventory.Items[ItemType.Timber]);
			}

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

        public override bool CheckRequiredItems(Person actor)
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

            person.AddNeed (ItemType.Wood, amountOfWoodNeeded, NeedEntry.Priority+1);
		}

		public void ConvertWoodToTimber(Person actor, decimal amountOfTimber)
		{
            if (Settings.IsVerbose)
                Console.WriteLine ("  Converting wood to timber");
            
			var woodNeeded = CalculateAmountOfWoodNeeded(amountOfTimber);

			// TODO: Is there a cleaner way to do this?
            if (actor.Inventory.Items [ItemType.Wood] >= woodNeeded) {
                TotalTimberMilled += amountOfTimber;
                if (Settings.IsVerbose) {
                    Console.WriteLine ("    Wood: " + woodNeeded);
                    Console.WriteLine ("    Timber: " + amountOfTimber);
                    Console.WriteLine ("    Total timber: " + TotalTimberMilled);
                }
                ItemsConsumed[ItemType.Wood] += woodNeeded;
                ItemsProduced[ItemType.Timber] += amountOfTimber;
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
		}

		public bool HasEnoughWood(decimal amountOfWoodNeeded)
		{	
			var value = Actor.Inventory.Has (ItemType.Wood, amountOfWoodNeeded);

			return value;
		}

		public decimal CalculateAmountOfWoodNeeded(decimal amountOfTimber)
		{
			return amountOfTimber * Settings.WoodRequiredForTimber;
		}
	}
}

