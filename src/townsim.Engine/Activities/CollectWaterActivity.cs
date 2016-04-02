using System;
using townsim.Engine.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
	[Serializable]
	public class CollectWaterActivity : BaseActivity
	{
		public decimal CollectionRate = 50.0m;
		
        public CollectWaterActivity (Person actor, NeedEntry needEntry, EngineSettings settings) : base(actor, needEntry, settings)
		{
		}

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            if (person.Inventory.IsFull(ItemType.Water)) {
               // if (!person.ActivityData.ContainsKey ("TotalWaterCollected")) {
               //     person.ActivityData ["TotalWaterCollected"] = 0m;
               // }

               // var amount = CollectionRate;

               /* if (IsComplete) { // If water is full, stop collecting
                    var total = (decimal)Person.ActivityData ["TotalWaterCollected"];
                    throw new NotImplementedException ();
                    //PlayerLog.WriteLine (CurrentEngine.Id, "Collected " + total + " water.");
                    //Finish ();
                }
                else*/
                throw new NotImplementedException ();
                //if (Person.Tile.HasItem(ItemType.Water, amount))
                //{
                //    ItemsProduced.Add(ItemType.Water, amount);

                    /*Tile.ItemsConsumed.Add (ItemType.Water, amount);

                    Person.ActivityData ["TotalWaterCollected"] = (decimal)Person.ActivityData ["TotalWaterCollected"] + amount;*/
               // }
            }
        }

        public override bool CheckFinished ()
        {
            throw new NotImplementedException ();
        }
        public override void ConfirmProduced (NeedEntry entry)
        {
            base.ConfirmProduced (entry);
        }
        public override bool CheckSupplies (Person actor)
        {
            throw new NotImplementedException ();
        }

        // TODO: Clean up
		/*protected override void ExecuteSingleCycle()
		{
			if (Person.ActivityType == ActivityType.CollectingWater) {
				
			}
		}

		public override void Start ()
		{
			throw new NotImplementedException ();
		}

		public override bool CheckComplete ()
		{
			return Person.Supplies [SupplyTypes.Water] >= Person.SuppliesMax [SupplyTypes.Water];
		}

		public override bool CheckImpossible ()
		{
			throw new NotImplementedException ();
		}

		public override void Finish ()
		{
			throw new NotImplementedException ();
		}*/
	}
}