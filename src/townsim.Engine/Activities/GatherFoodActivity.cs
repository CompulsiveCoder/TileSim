using System;
using townsim.Engine.Entities;
using townsim.Data;

namespace townsim.Engine.Activities
{
    [Serializable]
    [Activity(ItemType.Food)]
    public class GatherFoodActivity : BaseActivity
    {
        public decimal GatherionRate = 50.0m;

        public decimal TotalFoodGathered = 0;

        public GatherFoodActivity (Person actor, NeedEntry needEntry, EngineSettings settings) : base(actor, needEntry, settings)
        {
        }

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            if (Settings.IsVerbose)
                Console.WriteLine ("Gathering food");

            var personCanHoldMoreFood = !person.Inventory.IsFull (ItemType.Food);

            var tileHasFood = person.Tile.Inventory.Items [ItemType.Food] > 0;

            if (tileHasFood && personCanHoldMoreFood) {
                var amountThisCycle = Settings.DefaultGatherFoodRate;

                var tile = person.Tile;

                AddTransfer (tile, person, ItemType.Food, amountThisCycle);

                TotalFoodGathered += amountThisCycle;
            } else {
                if (Settings.IsVerbose)
                    Console.WriteLine ("  The tile has no food.");
            }
        }

        public override bool CheckFinished ()
        {
            return TotalFoodGathered >= NeedEntry.Quantity;
        }

        public override void ConfirmProduced (NeedEntry entry)
        {
            base.ConfirmProduced (entry);
        }

        public override bool CheckRequiredItems (Person actor)
        {
            if (actor.Tile == null)
                throw new Exception ("actor.Tile property is null.");

            var foodAvailable = actor.Tile.Inventory.Items [ItemType.Food] > 0;

            if (!foodAvailable && Settings.IsVerbose)
                Console.WriteLine ("  No food available.");

            return foodAvailable;
        }
    }
}