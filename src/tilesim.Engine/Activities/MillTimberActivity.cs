using System;
using tilesim.Engine.Activities;
using tilesim.Data;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;

namespace tilesim.Engine.Activities
{
	[Serializable]
    [Activity(ActionType.Mill, ItemType.Timber)]
	public class MillTimberActivity : BaseActivity
	{
		public decimal TotalTimberMilled = 0;

        public MillTimberActivity (Person person, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
			: base(person, needEntry, settings, console)
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
				Console.WriteDebugLine ("Starting " + GetType ().Name + " activity");
				Console.WriteDebugLine ("  Quantity (timber): " + NeedEntry.Quantity);
				Console.WriteDebugLine ("  Current (timber): " + person.Inventory.Items[ItemType.Timber]);
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
                Console.WriteDebugLine ("  Registering the need to " + ActionType.Fell + "  " + amountOfWoodNeeded + " wood");

            person.AddNeed (ActionType.Fell, ItemType.Wood, amountOfWoodNeeded, NeedEntry.Priority+1);
		}

		public void ConvertWoodToTimber(Person actor, decimal amountOfTimber)
		{
            Status = "Milling wood into timber " + TotalTimberMilled;

            if (Settings.IsVerbose)
                Console.WriteDebugLine ("  Converting wood to timber");
            
			var woodNeeded = CalculateAmountOfWoodNeeded(amountOfTimber);

			// TODO: Is there a cleaner way to do this?
            if (actor.Inventory.Items [ItemType.Wood] >= woodNeeded) {
                TotalTimberMilled += amountOfTimber;
                if (Settings.IsVerbose) {
                    Console.WriteDebugLine ("    Wood: " + woodNeeded);
                    Console.WriteDebugLine ("    Timber: " + amountOfTimber);
                    Console.WriteDebugLine ("    Total timber: " + TotalTimberMilled);
                }
                ItemsConsumed[ItemType.Wood] += woodNeeded;
                ItemsProduced[ItemType.Timber] += amountOfTimber;
            } else {
                if (Settings.IsVerbose) {
                    Console.WriteDebugLine ("    Not enough wood: " + woodNeeded);
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

