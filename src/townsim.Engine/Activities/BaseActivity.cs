using System;
using townsim.Engine.Entities;
using townsim.Engine.Needs;
using System.Collections.Generic;

namespace townsim.Engine
{
	public abstract class BaseActivity
	{
		public bool IsFinished { get;set; }

		public EngineSettings Settings { get;set; }

		public Person Actor { get; set; }

		public NeedEntry NeedEntry { get; set; }

		public Dictionary<ItemType, decimal> ItemsProduced = new Dictionary<ItemType, decimal>();
        public Dictionary<ItemType, decimal> ItemsConsumed = new Dictionary<ItemType, decimal>();
        public List<ItemTransfer> Transfers = new List<ItemTransfer>();
        public Dictionary<PersonVital, decimal> VitalsChange = new Dictionary<PersonVital, decimal> ();
        public List<NeedEntry> Needs = new List<NeedEntry>();

        public ConsoleHelper Console { get; set; }

		public string Name
		{
			get { return GetType ().Name; }
		}

        public BaseActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
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
            var hasSupplies = CheckRequiredItems (person);
            if (hasSupplies)
                Execute (person);
            
            CommitActivityResults ();

			var isFinished = CheckFinished ();
			if (isFinished)
				Finish ();
		}

		public abstract void Prepare(Person person);

		public abstract void Execute (Person person);

		public abstract bool CheckFinished();

        public abstract bool CheckRequiredItems (Person actor);

		public virtual void Finish()
		{
            if (!IsFinished) {
                if (Settings.IsVerbose)
                    Console.WriteDebugLine ("  Activity finished.");
			
                IsFinished = true;

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
            if (Settings.IsVerbose)
                Console.WriteDebugLine ("    Produced:");
            
            foreach (var itemType in ItemsProduced.Keys)
            {
                var amountProduced = ItemsProduced [itemType];

                if (amountProduced > 0) {
                    if (Settings.IsVerbose)
                        Console.WriteDebugLine ("      " + itemType + ": " + amountProduced);

                    Actor.Inventory.Items [itemType] += amountProduced;
                }
            }

            ItemsProduced.Clear ();
            InitializeItemsProduced ();
        }

        public void CommitConsumed()
        {
            if (Settings.IsVerbose) {
                Console.WriteDebugLine ("    Consumed:");
            }

            foreach (var itemType in ItemsConsumed.Keys)
            {
                var amountConsumed = ItemsConsumed [itemType];

                if (amountConsumed > 0) {
                    if (Settings.IsVerbose)
                        Console.WriteDebugLine ("      " + itemType + ": " + amountConsumed);

                    Actor.Inventory.Items [itemType] -= amountConsumed;
                }
            }

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

        public void AddNeed(ActionType actionType, ItemType itemType, decimal quantity, decimal priority)
        {
            if (Settings.IsVerbose)
                Console.WriteDebugLine ("    Registering the need to " + actionType + "  " + quantity + " " + itemType + ".");
            
            Needs.Add (new NeedEntry (actionType, itemType, quantity, priority));
        }
	}
}

