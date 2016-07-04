using System;
using tilesim.Engine;

namespace tilesim.Engine
{
    public interface IHasInventory
    {
        Inventory Inventory { get; }
    }
}

