using System;
using townsim.Engine.Entities;

namespace townsim.Engine.Transfers
{
    public class TransferManager
    {
        // TODO: Remove if not neede
        public TransferManager ()
        {
        }

        public void Transfer(ItemType itemType, decimal amount, IHasInventory target)
        {
            throw new NotImplementedException ();
            /*if (Parent == null)
                throw new Exception ("Parent property is null");

            if (Settings.IsVerbose) {
                Console.WriteLine ("Executing transfer between inventories.");
                Console.WriteLine ("  Source type: " + Parent.GetType().Name);
                Console.WriteLine ("  Target type: " + target.GetType().Name);
                Console.WriteLine ("  Item type: " + itemType);
                Console.WriteLine ("  Amount: " + amount);
            }

            target.Inventory.Items [itemType] += amount;
            Items [itemType] -= amount;*/
        }
    }
}

