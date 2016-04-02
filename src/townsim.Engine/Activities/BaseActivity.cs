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
		public Dictionary<ItemType, decimal> NeedsConsumed = new Dictionary<ItemType, decimal>();

		public string Name
		{
			get { return GetType ().Name; }
		}

		public BaseActivity (Person actor, NeedEntry needEntry, EngineSettings settings)
		{
			Actor = actor;
			NeedEntry = needEntry;
			Settings = settings;

            ItemsProduced.Add (ItemType.Shelter, 0);
            ItemsProduced.Add (ItemType.Food, 0);
            ItemsProduced.Add (ItemType.Water, 0);
            ItemsProduced.Add (ItemType.Timber, 0);
            ItemsProduced.Add (ItemType.Wood, 0);

            NeedsConsumed.Add (ItemType.Shelter, 0);
            NeedsConsumed.Add (ItemType.Food, 0);
            NeedsConsumed.Add (ItemType.Water, 0);
            NeedsConsumed.Add (ItemType.Timber, 0);
            NeedsConsumed.Add (ItemType.Wood, 0);
		}

		public void Act(Person person)
		{
            var hasSupplies = CheckSupplies (person);
            if (hasSupplies)
			    Execute (person);	

			var isFinished = CheckFinished ();
			if (isFinished)
				Finish ();
		}

		public abstract void Prepare(Person person);

		public abstract void Execute (Person person);

		public abstract bool CheckFinished();

        public abstract bool CheckSupplies (Person actor);

		public virtual void Finish()
		{
            if (!IsFinished) {
                if (Settings.IsVerbose)
                    Console.WriteLine ("  Activity finished.");
			
                IsFinished = true;

                Actor.Needs.Remove (NeedEntry);

                CommitActivityResults ();

                Actor.ActivityQueue.Remove (this);
            }
		}

		public void CommitActivityResults()
		{
			if (Settings.IsVerbose) {
				Console.WriteLine ("  Committing activity results...");

				Console.WriteLine ("    Produced:");
			}

			foreach (var need in ItemsProduced.Keys)
			{
				var amountProduced = ItemsProduced [need];

                if (amountProduced > 0) {
                    if (Settings.IsVerbose)
                        Console.WriteLine ("      " + need + ": " + amountProduced);
				
                    Actor.Inventory.Items [need] += amountProduced;
                }
			}

			if (Settings.IsVerbose) {
				Console.WriteLine ("    Consumed:");
			}

			foreach (var need in NeedsConsumed.Keys)
			{
				var amountConsumed = NeedsConsumed [need];

                if (amountConsumed > 0) {
                    if (Settings.IsVerbose)
                        Console.WriteLine ("      " + need + ": " + amountConsumed);
				
                    Actor.Inventory.Items [need] -= amountConsumed;
                }
			}

		}

        public virtual void ConfirmProduced(NeedEntry entry)
        {
            ItemsProduced [entry.Type] += entry.Quantity;
        }
		// TODO: Remove if not needed
		/*public virtual void SetQuantity(decimal quantity)
		{
			Quantity = quantity;
		}

		public virtual void SetNeedEntry(NeedEntry needEntry)
		{
			NeedEntry = needEntry;
		}*/
	}
}

