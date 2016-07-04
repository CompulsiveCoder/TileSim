using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine.Transfers
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
                Console.WriteDebugLine ("Executing transfer between inventories.");
                Console.WriteDebugLine ("  Source type: " + Parent.GetType().Name);
                Console.WriteDebugLine ("  Target type: " + target.GetType().Name);
                Console.WriteDebugLine ("  Item type: " + itemType);
                Console.WriteDebugLine ("  Amount: " + amount);
            }

            target.Inventory.Items [itemType] += amount;
            Items [itemType] -= amount;*/
        }
    }
}

