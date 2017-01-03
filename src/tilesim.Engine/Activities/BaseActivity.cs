using System;
using tilesim.Engine.Entities;
using tilesim.Engine.Needs;
using System.Collections.Generic;

namespace tilesim.Engine.Activities
{
    [Serializable]
	public abstract class BaseActivity
	{
        private bool isFinished;
		public bool IsFinished
        {
            get { return isFinished; }
        }

		public EngineSettings Settings { get;set; }

		public Person Actor { get; set; }

		public NeedEntry NeedEntry { get; set; }

		public Dictionary<ItemType, decimal> ItemsProduced = new Dictionary<ItemType, decimal>();
        public Dictionary<ItemType, decimal> ItemsConsumed = new Dictionary<ItemType, decimal>();
        public List<ItemTransfer> Transfers = new List<ItemTransfer>();
        public Dictionary<PersonVitalType, decimal> VitalsChange = new Dictionary<PersonVitalType, decimal> ();
        public List<NeedEntry> Needs = new List<NeedEntry>();

        public ConsoleHelper Console { get; set; }

        public string Text
        {
            get { return NeedEntry.ActionType + " " + NeedEntry.ItemType; }
        }

        private decimal percentComplete;
        public decimal PercentComplete
        {
            get { return percentComplete; }
        }

		public string Name
		{
			get { return GetType ().Name; }
		}

        public string Status { get;set; }

        public BaseActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
		{
            Construct (actor, needEntry, settings, console);
		}

        public void Construct(Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
        {
            Actor = actor;
            NeedEntry = needEntry;
            Settings = settings;
            Console = console;

            InitializeItemsProduced ();
            InitializeItemsConsumed ();

        }

        public void InitializeItemsProduced()
        {
            ItemsProduced.Add (ItemType.Shelter, 0);
            ItemsProduced.Add (ItemType.Food, 0);
            ItemsProduced.Add (ItemType.Water, 0);
            ItemsProduced.Add (ItemType.Timber, 0);
            ItemsProduced.Add (ItemType.Wood, 0);
        }

        public void InitializeItemsConsumed()
        {
            ItemsConsumed.Add (ItemType.Shelter, 0);
            ItemsConsumed.Add (ItemType.Food, 0);
            ItemsConsumed.Add (ItemType.Water, 0);
            ItemsConsumed.Add (ItemType.Timber, 0);
            ItemsConsumed.Add (ItemType.Wood, 0);
        }

		public void Act(Person person)
		{
            Console.WriteDebugLine ("Starting action: " + Name + "   BaseActivity.Act(Person)");

            var canAct = IsActorAbleToAct (person);
            if (canAct) {
                Console.WriteDebugLine ("  User is able to act");

                WriteNeedDebugConsoleOutput ();

                Execute (person);
            
                // Does this function need to include a call to CommitNeeds as well? It shouldn't be necessary. It's called if the person can't act.
                CommitActivityResults ();

                var isFinished = CheckFinished ();
                if (isFinished)
                    Finish ();
            } else {
                Console.WriteDebugLine ("  Unable to act");

                RegisterNeeds (person);

                // IMPORTANT: Commit the person's needs at the end of the activity
                CommitNeeds ();
            }
		}

        protected void WriteNeedDebugConsoleOutput()
        {
            Console.WriteDebugLine ("  Need:");
            Console.WriteDebugLine ("    Item: " + NeedEntry.ItemType);
            Console.WriteDebugLine ("    Quantity: " + NeedEntry.Quantity);
            Console.WriteDebugLine ("    Vital: " + NeedEntry.VitalType);
        }

		public abstract void Prepare(Person person);

		public abstract void Execute (Person person);

		public abstract bool CheckFinished();

        public abstract void RegisterNeeds (Person actor);

        public abstract bool IsActorAbleToAct (Person actor);

		public virtual void Finish()
		{
            if (!IsFinished) {
                if (Settings.IsVerbose)
                    Console.WriteDebugLine ("  Activity finished.");
			
                MarkAsFinished();

                Actor.Needs.Remove (NeedEntry);

                Actor.ActivityQueue.Remove (this);
            }
		}

		public void CommitActivityResults()
		{
			if (Settings.IsVerbose) {
				Console.WriteDebugLine ("  Committing activity results...");
			}

            CommitProduced ();

            CommitConsumed ();

            CommitTransfers ();

            CommitVitalsChanges ();

            CommitNeeds ();
		}

        public void CommitProduced()
        {
            Console.WriteDebugLine ("    Produced:");

            var wasZeroProduced = true;

            foreach (var itemType in ItemsProduced.Keys)
            {
                var amountProduced = ItemsProduced [itemType];

                if (amountProduced > 0) {
                    wasZeroProduced = false;

                    if (Settings.IsVerbose)
                        Console.WriteDebugLine ("      " + itemType + ": " + amountProduced);

                    Actor.Inventory.Items [itemType] += amountProduced;
                }
            }

            if (wasZeroProduced)
                Console.WriteDebugLine ("      [nothing]");

            ItemsProduced.Clear ();
            InitializeItemsProduced ();
        }

        public void CommitConsumed()
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("    Consumed:");
            }

            var wasZeroConsumed = true;

            foreach (var itemType in ItemsConsumed.Keys)
            {
                var amountConsumed = ItemsConsumed [itemType];

                if (amountConsumed > 0) {
                    wasZeroConsumed = false;
                    if (Settings.IsVerbose)
                        Console.WriteDebugLine ("      " + itemType + ": " + amountConsumed);

                    Actor.Inventory.Items [itemType] -= amountConsumed;
                }
            }

            if (wasZeroConsumed)
                Console.WriteDebugLine ("      [nothing]");

            ItemsConsumed.Clear ();
            InitializeItemsConsumed ();
        }

        public void CommitTransfers()
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("  Committing transfers");
            }

            foreach (var transfer in Transfers) {
                CommitTransfer (transfer);
            }

            Transfers.Clear ();
        }

        public void CommitTransfer(ItemTransfer transfer)
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("    Committing transfer");
                Console.WriteDebugLine ("      Source: " + transfer.Source.GetType().Name);
                Console.WriteDebugLine ("      Destination: " + transfer.Destination.GetType().Name);
                Console.WriteDebugLine ("      Type: " + transfer.Type);
                Console.WriteDebugLine ("      Quantity: " + transfer.Quantity);
            }

            var type = transfer.Type;

            transfer.Source.Inventory [type] -= transfer.Quantity;
            transfer.Destination.Inventory [type] += transfer.Quantity;      

            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("      Source (" + transfer.Source.GetType().Name + ") total: " + transfer.Source.Inventory[type]);
                Console.WriteDebugLine ("      Destination (" + transfer.Destination.GetType().Name + ") total: " + transfer.Destination.Inventory[type]);
            }
        }

        public void CommitVitalsChanges()
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("    Committing vitals changes");
            }

            foreach (var vital in VitalsChange.Keys) {
                var changeValue = VitalsChange [vital];
                var previousValue = Actor.Vitals [vital];
                var newValue = previousValue + changeValue;

                newValue = PercentageValidator.Validate (newValue);

                if (Settings.IsVerbose) {
                    Console.WriteDebugLine ("      " + vital);
                    Console.WriteDebugLine ("        Previous: " + previousValue);
                    Console.WriteDebugLine ("        Change: " + changeValue);
                    Console.WriteDebugLine ("        New value: " + newValue);
                }
                Actor.Vitals [vital] = newValue;
            }

            VitalsChange.Clear ();
        }

        public void CommitNeeds()
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("    Committing needs");
            }

            while (Needs.Count > 0)
            {
                var need = Needs [0];

                Actor.Needs.Add (need);

                Needs.RemoveAt (0);               
            }
        }

        public virtual void ConfirmProduced(NeedEntry entry)
        {
            ItemsProduced [entry.ItemType] += entry.Quantity;
        }

        public void AddTransfer(IHasInventory source, IHasInventory destination, ItemType itemType, decimal quantity)
        {
            Transfers.Add (new ItemTransfer(source, destination, itemType, quantity));
        }

        public void AddNeed(ActivityVerb actionType, ItemType itemType, PersonVitalType vitalType, decimal quantity, decimal priority)
        {
            Console.WriteDebugLine ("    Registering the need to " + actionType + "  " + quantity + " " + itemType + ".");
            
            Needs.Add (new NeedEntry (actionType, itemType, vitalType, quantity, priority));

            Console.WriteDebugLine ("    Total needs (after): " + Needs.Count);
        }

        public void MarkAsFinished()
        {
            Console.WriteDebugLine ("    Marking activity as finished");
            percentComplete = 100;
            isFinished = true;
        }

        public void SetPercentComplete(decimal percentComplete)
        {
            var validatedPercentComplete = PercentageValidator.Validate (percentComplete);

            Console.WriteDebugLine ("    Setting \"Percent Complete\" to " + validatedPercentComplete);

            this.percentComplete = validatedPercentComplete;
        }

        public void IncreasePercentComplete(decimal percentageIncrease)
        {
            Console.WriteDebugLine ("    Increasing \"Percent Complete\" by " + percentageIncrease);

            percentComplete += percentageIncrease;

            percentComplete = PercentageValidator.Validate (percentComplete);
        }

        public override string ToString ()
        {
            var quantityToProduce = NeedEntry.Quantity;

            return string.Format ("{0} {1} ({2}) {3}",
                NeedEntry.ActionType,
                (NeedEntry.ItemType != ItemType.NotSet ? NeedEntry.ItemType.ToString() : ""),
                (int)quantityToProduce,
                Status);
        }
	}
}

