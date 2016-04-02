using System;
using System.Collections.Generic;
using townsim.Engine.Entities;

namespace townsim.Engine
{
    [Serializable]
    public class Inventory
    {
        public decimal this[ItemType type]
        {
            get { return Items [type]; } 
            set { Items [type] = value; }
        }

        public Dictionary<ItemType, decimal> Items = new Dictionary<ItemType, decimal> ();

        public Dictionary<ItemType, decimal> ItemLimits = new Dictionary<ItemType, decimal> ();
        
        public DemandList Demands { get; set; }

        public EngineSettings Settings { get;set; }

        public IHasInventory Parent { get; set; }

        public Inventory()
        {
        }

        public Inventory (IHasInventory parent, EngineSettings settings)
        {
            Settings = settings;

            Parent = parent;

            Construct ();
        }

        public Inventory (IHasInventory parent, DemandList demands, EngineSettings settings)
        {
            Parent = parent;

            Settings = settings;

            Demands = demands;

            Construct ();
        }

        public void Construct()
        {
            Items.Add (ItemType.Shelter, 0);
            ItemLimits.Add (ItemType.Shelter, 1);

            Items.Add (ItemType.Food, 0);
            ItemLimits.Add (ItemType.Food, 1000);

            Items.Add (ItemType.Water, 0);
            ItemLimits.Add (ItemType.Water, 1000);

            Items.Add (ItemType.Wood, 0);
            ItemLimits.Add (ItemType.Wood, 1000);

            Items.Add (ItemType.Timber, 0);
            ItemLimits.Add (ItemType.Timber, 1000);
        }

        public void AddItem(ItemType itemType, decimal amount)
        {
            Items [itemType] = (decimal)Items [itemType] + amount;

            if (Demands != null && Demands.GetDemandAmount(itemType) > 0)
                Demands.RemoveDemand (itemType, amount);
        }

        public void RemoveItem(ItemType itemType, decimal amount)
        {
            if (amount > Items[itemType])
                throw new Exception ("There's not enough available. item " + amount + " but there's only " + Items[itemType] + ".");

            Items [itemType] = Items [itemType] - amount;

            if (Items[itemType] < 0)
                Items[itemType] = 0;
        }

        public bool Has(ItemType itemType, decimal amount)
        {
            var value = Items [itemType] >= amount;
            return value;
        }

        public bool IsFull(ItemType itemType)
        {
            return Items [itemType] >= ItemLimits [itemType];
        }

    }
}

