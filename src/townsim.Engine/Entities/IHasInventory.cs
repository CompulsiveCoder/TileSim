using System;
using townsim.Engine;

namespace townsim.Engine
{
    public interface IHasInventory
    {
        Inventory Inventory { get; }
    }
}

