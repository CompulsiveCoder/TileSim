using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Activities
{
	[Serializable]
    [Activity(ActivityVerb.Gather, ItemType.Water, PersonVitalType.NotSet)]
	public class GatherWaterActivity : BaseActivity
	{
		public decimal CollectionRate = 50.0m;

        public decimal TotalWaterCollected = 0;
        		
        public GatherWaterActivity (Person actor, NeedEntry needEntry, EngineSettings settings, ConsoleHelper console)
            : base(actor, needEntry, settings, console)
		{
		}

        public override void Prepare (Person person)
        {
            throw new NotImplementedException ();
        }

        public override void Execute (Person person)
        {
            if (Settings.IsVerbose)
                Console.WriteDebugLine ("Collecting water");

            var personCanHoldMoreWater = !person.Inventory.IsFull (ItemType.Water);

            var tileHasWater = person.Tile.Inventory.Items [ItemType.Water] > 0;

            if (tileHasWater && personCanHoldMoreWater) {
                var amountThisCycle = Settings.DefaultCollectWaterRate;

                var tile = person.Tile;

                AddTransfer (tile, person, ItemType.Water, amountThisCycle);

                TotalWaterCollected += amountThisCycle;
            } else {
                if (Settings.IsVerbose)
                    Console.WriteDebugLine ("  The tile has no water.");
            }
        }

        public override bool CheckFinished ()
        {
            return TotalWaterCollected >= NeedEntry.Quantity;
        }

        public override void ConfirmProduced (NeedEntry entry)
        {
            base.ConfirmProduced (entry);
        }

        public override bool CanAct (Person actor)
        {
            if (actor.Tile == null)
                throw new Exception ("actor.Tile property is null.");
            
            var waterAvailable = actor.Tile.Inventory.Items [ItemType.Water] > 0;

            if (!waterAvailable && Settings.IsVerbose)
                Console.WriteDebugLine ("  No water available.");

            return waterAvailable;
        }
	}
}