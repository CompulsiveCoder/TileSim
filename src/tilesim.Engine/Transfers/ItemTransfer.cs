using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
    public class ItemTransfer
    {
        public IHasInventory Source { get; set; }

        public IHasInventory Destination { get; set; }

        public ItemType Type { get; set; }

        public decimal Quantity { get; set; }

        public ItemTransfer (IHasInventory source, IHasInventory destination, ItemType itemType, decimal quantity)
        {
            Source = source;
            Destination = destination;
            Type = itemType;
            Quantity = quantity;
        }
    }
}

